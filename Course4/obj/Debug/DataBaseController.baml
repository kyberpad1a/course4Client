   M S B A M L   `   `   `  ????B  >Course4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Course4Course4  5           Course4.DataBaseController   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Course4     5        N? Title$  DataBaseController??5	      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $
??267* ?6   ?? 5      $
??266* ?6   ?? 5      $
??150* ?6   ?? 5      $
??117* ?6    ? RowDefinitions 5   
   ?? 5      $	??24* ?6   ?? 5      $	??33* ?6   ?? 5      $	??57* ?6   ?? 5      $	??56* ?6   ?? 5      $	??56* ?6   ?? 5      $	??56* ?6   ?? 5      $	??56* ?6   ?? 5      $	??56* ?6   ?? 5      $	??56* ?6   .+?5   
   ?? -   5        $??btnBack??5   1   $??3??6   $??
Назад??6!   6@   ?? 5   
   -   5        $??
btnRezCopy??5   P   $??0??6   $??4??6!   $2??,Создать резервную копию??6.   $??0,0.4,0.2,55.6q?6{   $??2??6?   ?? 5   
   -   5        $??	btnBackup??5   P   $??2??6   $??4??6!   $,??&Выполнить Backup данных??6.   $??0.4,0.4,0,55.6q?6y   $??2??6?   $??2??6?   ?? 5    
   -   5        $??btnRollback??5    Q   $??7??6   $Q??KВыполнить откат данных по выбранной дате??6   $??3??6~   $??267,0.4,149.333,55.6q?6?   $??2??6?   ?? 5!   
   -   5        $??cbDateRollback??5!   J   $??1??6   $??6??6#   $??9.8,10.4,9.6,9.6q?60   6J   5"      5#      