using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Course4
{
    /// <summary>
    /// Логика взаимодействия для DataBaseController.xaml
    /// </summary>
    public partial class DataBaseController : Page
    {
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            ConString connection = new ConString();
        public NpgsqlConnection connect { get; }

        public DataBaseController()
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            if (DateTime.Now.ToString("dd") == "12")
            {
                Backup((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", "") + $@"\{DateTime.Now.ToString("d").Replace(".", "")}.backup");
            }
            DirectoryInfo dir = new DirectoryInfo((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", ""));
            FileInfo[] Files = dir.GetFiles();
            foreach (FileInfo file in Files)
                cbDateRollback.Items.Add(file.Name.Insert(2, ".").Insert(5, ".").Replace(".backup", ""));
        }

        string strPG_dumpPath = "SET PGPASSWORD=3785\r\n\r\ncd /D C:\\Program Files\r\n\r\ncd PostgreSQL\r\n\r\ncd 13\r\n\r\ncd bin\r\n\r\n";
        string strServer = "192.168.73.225";
        string strPort = "5432";
        string strDatabaseName = "Course4";
        /// <summary>
        /// Создание файла с резервной копией бд
        /// </summary>
        /// <param name="pathSave">Путь для сохранения</param>
        public void Backup(string pathSave)
        {
            try
            {
                strDatabaseName = "Course4";
                StreamWriter sw = new StreamWriter("DBBackup.bat");
                // Do not change lines / spaces b/w words.
                StringBuilder strSB = new StringBuilder(strPG_dumpPath);

                if (strSB.Length != 0)
                {
                    
                    strSB.Append("pg_dump.exe --host " + strServer + " --port " + strPort + " --username postgres --format custom --blobs --verbose --file ");
                    strSB.Append("\"" + pathSave + "\"");
                    strSB.Append(" \"" + strDatabaseName + "\"" + "\r\n\r\n");
                    sw.WriteLine(strSB);
                    sw.Dispose();
                    sw.Close();
                    Process processDB = Process.Start("DBBackup.bat");
                    MessageBox.Show("Резервная копия успешно сохранена");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, укажите место для создания резервной копии");
                }
            }
            catch
            { }
        }
        /// <summary>
        /// Создание бэкапа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnRezCopy_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "BACKUP Files|*.backup*"
            };
            dialog.Title = "Save as backup file";
            if (dialog.ShowDialog() == true)
            {
                Backup(dialog.FileName + ".backup");
            }


        }
        /// <summary>
        /// Восстановление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "BACKUP Files|*.backup*"
            };
            dialog.Title = "Choose backup file";
            if (dialog.ShowDialog() == true)
            {
                Restore(dialog.FileName);
            }
        }
        /// <summary>
        /// Откат данных
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnRollback_Click(object sender, RoutedEventArgs e)
        {
            Restore((System.IO.Directory.GetCurrentDirectory() + @"\BackupFiles").Replace(@"\bin", "").Replace(@"\Debug", "") + @"\" + cbDateRollback.SelectedItem.ToString().Replace(".", "") + ".backup");
        }
        /// <summary>
        /// Восстановление базы данных
        /// </summary>
        /// <param name="pathFile">Путь до бэкапа</param>
        public void Restore(string pathFile)
        {
            strDatabaseName = "Course4Restore";
            NpgsqlCommand command = new NpgsqlCommand();
            connect.Open();
            try
            {
                if (new NpgsqlCommand("SELECT 1 as count FROM pg_database WHERE datname='Course4Restore'", connect).ExecuteScalar().ToString() == "1")
                {
                    command = new NpgsqlCommand("DROP DATABASE \"Course4Restore\";", connect);
                    command.ExecuteNonQuery();
                }
                command = new NpgsqlCommand("Create DATABASE \"Course4Restore\";", connect);
                command.ExecuteNonQuery();
                connect.Close();

                if (strDatabaseName != "")
                {
                    if (pathFile != "")
                    {
                        StreamWriter sw = new StreamWriter("DBRestore.bat");
                        // Do not change lines / spaces b/w words.
                        StringBuilder strSB = new StringBuilder(strPG_dumpPath);
                        if (strSB.Length != 0)
                        {
                            strSB.Append("pg_restore.exe --host " + strServer +
                            " --port " + strPort + " --username postgres --dbname");
                            strSB.Append(" \"" + strDatabaseName + "\"");
                            strSB.Append(" --verbose ");
                            strSB.Append("\"" + pathFile + "\"");
                            sw.WriteLine(strSB);
                            sw.Dispose();
                            sw.Close();
                            Process processDB = Process.Start("DBRestore.bat");
                            MessageBox.Show("Успешный backup данных");
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, введите путь сохранения, чтобы получить резервную копию");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the Database name to Restore!");
                }
            }
            catch { MessageBox.Show("База данных занята другой программой"); connect.Close(); }
        }
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.GoBack(); //Фокусы
        }
    }
}
