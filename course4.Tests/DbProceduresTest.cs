using Course4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;

namespace course4.Tests
{
    [TestClass]
    public class DbProceduresTest
    {
        ConString connection = new ConString();
        public NpgsqlConnection connect { get; set; }

        string warehouse_address = "Mendeleevskaya";
        [TestMethod]
        public void WarehouseInsert_Mendeleevskaya_1returned()
        {
            connect = new NpgsqlConnection(connection.constring);
            //arange
            string expected = "1";
            //act
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"Call Warehouse_Insert('{warehouse_address}')", connect);
            command.ExecuteNonQuery();
            command = new NpgsqlCommand($@"select count(warehouse_address) from warehouse where warehouse_address = '{warehouse_address}'", connect);
            string actual = command.ExecuteScalar().ToString();
            connect.Close();
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WarehouseUpdate_Mendeleevskaya_Petrovskaya_1returned()
        {
            connect = new NpgsqlConnection(connection.constring);
            //arange
            string expected = "1";
            //act
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($@"select id_warehouse from warehouse where warehouse_address = '{warehouse_address}'", connect);
            warehouse_address = "Petrovskaya";
            command = new NpgsqlCommand($@"Call Warehouse_Update({command.ExecuteScalar().ToString()},'{warehouse_address}')", connect);
            command.ExecuteNonQuery();
            command = new NpgsqlCommand($@"select count(warehouse_address) from warehouse where warehouse_address = '{warehouse_address}'", connect);
            string actual = command.ExecuteScalar().ToString();
            connect.Close();
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WarehouseZelete_Petrovskaya_0returned()
        {
            connect = new NpgsqlConnection(connection.constring);
            //arange
            string expected = "0";
            //act
            connect.Open();
            warehouse_address = "Petrovskaya";
            NpgsqlCommand command = new NpgsqlCommand($@"select id_warehouse from warehouse where warehouse_address = '{warehouse_address}'", connect);
            command = new NpgsqlCommand($@"call Warehouse_Delete('{command.ExecuteScalar().ToString()}')", connect);
            command.ExecuteNonQuery();
            command = new NpgsqlCommand($@"select count(warehouse_address) from warehouse where warehouse_address = '{warehouse_address}'", connect);
            string actual = command.ExecuteScalar().ToString();
            connect.Close();
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
