   M S B A M L   `   `   `  ????B  >Course4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Course4Course4  5          Course4.GoodsStaff   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Course4     5        N? Title$  
GoodsStaff??5	      .??5      ? 
??????? ? RowDefinitions 5   
   ?? 5      $	??42* ?6   ?? 5      $	??63* ?6   ?? 5      $	??34* ?6   ?? 5      $	??36* ?6   ?? 5      $??5* ?6   ?? 5      $	??28* ?6   ?? 5      $	??78* ?6   ?? 5      $
??110* ?6   ?? 5      $
??110* ?6   ?? 5      $	??53* ?6   ?? 5      $	??64* ?6   ?? 5      $	??52* ?6   ?? 5      $	??41* ?6   ?? 5      $	??35* ?6   ?? 5      $	??36* ?6   ?? 5      $	??42* ?6   ?? 5      $	??36* ?6   ?? 5      $	??42* ?6   ?? 5      $	??36* ?6   ?? 5       $	??42* ?6   ?? 5!      $
??112* ?6    ? ColumnDefinitions 5#   
   ?? 5$      $	??35* ?6   ?? 5%      $
??138* ?6   ?? 5&      $
??138* ?6   ?? 5'      $
??326* ?6   ?? 5(      $
??358* ?6   ?? 5)      $
??195* ?6   ?? 5*      $	??20* ?6   ?? 5+      $	??28* ?6   ?? 5,      $
??160* ?6   ?? 5-      $	??20* ?6   ?? 5.      $
??174* ?6   ?? 5/      $	??20* ?6   ?? 50      $
??130* ?6   ?? 51      $
??130* ?6   ?? 53      $	??50* ?6   .+?56   
   &   System.Windows.Controls.DataGrid  -   5        $??dg_goodsstaff??56      $??5??6(   $??13??65   $??8??6J      
IsReadOnly . $??1??6n   6~   ?? 57   
   -   5         ??Name$ btn_back??57      $??1??6#   $??3??63   $
??🡸??6A   $??20(?6f   $	??Top=?6t   
???????6?   ?? 58   
   -   5        $ btn_add??58      $??5??6"   $??3??62   $??Добавить??6@   6T   ?? 59   
   -   5        $ 
btn_export??59      $??12??6%   $??3??66   $??Экспорт??6C   $??2??6U   6i   ?? 5:   
   -   5        $ btn_upd??5:      $??7??6"   $??3??62   $??Изменить??6@   $??2??6j   ?? 5;   
   -   5        $ btn_del??5;      $??10??6"   $??3??63   $??Удалить??6A   6T   ?? 5<   
   -   5         ??Name$ tb_src??5<      $??3??6"   $??3??6/   $??2??6?   6T   ?? 5=   
   $??Поиск:??6   $??3??6!   $??2??6.   $??Right=?6>   $??12(?6[   ?? 5>   
   -   5        $??	btn_Graph??5>      $??1??6"   $??2??6/   $??4??6C   $??Товары??6S   $??Center=?6d   $??Center=?6   $??40(?6?   6?   ?? 5?   
   $??14??6   $??2??6   $#??Название товара??6.   $??12(?6H   $??2??6V   ?? 5@   
   -	   5        $??tb_goodname??5@      $??15??6%   $??2??63   $??2??6C   ?? 5A   
   $??16??6   $??2??6   $#??Материал товара??6.   $??12(?6H   $??2??6V   ?? 5B   
   -
   5        $??tb_goodmaterial??5B      $??17??6)   $??2??67   $??2??6G   ?? 5C   
   $??18??6   $??2??6   $??Цена??6.   $??12(?6=   $??2??6K   ?? 5D   
   -   5        $??tb_price??5D      $??19??6"   $??2??60   $??2??6@   ?? 5E   
   $??14??6   $??5??6   $??Сертификат??6.   $??12(?6C   $??2??6Q   ?? 5F   
   -   5        $??cb_certificate??5F      $??15??6)   $??5??67   $??4??6G   5G      5H      