   M S B A M L   `   `   `  ����Q  MOverlayImageForWindows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:OverlayImageForWindowsOverlayImageForWindows  5   	   '    !OverlayImageForWindows.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:OverlayImageForWindows     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � Topmost .  � WindowStyle$
 None=�5	   	    � AllowsTransparency . 
������ $	��150��5       $	��150��60    � WindowStartupLocation$ CenterScreen=�6?    � ShowInTaskbar .  .��5      � -   5        $��MainGrid��5       � RowDefinitions 5   
   �� 5      $	��150 �6   6+   �� 5      $��1* �6   6*    � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $	��150 �6   6-   .+�5   
   �� -   5        $��	MainImage��5      $��/Resourses/Main.jpg��6!   $
��Fill=�6>   $��2��6M   $��2��6^    J� 	AllowDrop . 5      r� 5   
   -   5        $��	MainVideo��5      	 r� Stretch$
	 Fill=�6(   $��2��67   $��2��6H   $y�Hidden=�6\   
 r� Volume$
 0Q�5       .  r� LoadedBehavior$ Manual=�5#       r� UnloadedBehavior$
 Play=�5$      5%      n� 5)   
   -   5         n�Name$ 	LeftThumb��5)      $��20��6#   $��2��6.    J� Opacity$ 0Q�6V    J� IsHitTestVisible . $
��Left=�5*      $��Stretch=�63   ��5)   @   Z� ��5*   p   Z� n� 5+   
   -   5        $ 
RightThumb��5+      $��20��6$   
������ $��2��6H   $��2��6X   
������ $ 0Q�6�    . $��Right=�5,      $��Stretch=�64   ��6r   Z� n� 5-   
   -   5        $ TopThumb��5-      $��20��6"   $��2��6D   
������ $ 0Q�6s    . $��Stretch=�5.      $	��Top=�66   ��5-   .   Z� ��5.   n   Z� n� 5/   
   -   5        $ BottomThumb��5/      $��20��6%   $��2��6G   $��2��6[   
������ $ 0Q�6�    . $��Stretch=�50      $��Bottom=�66   ��5/   1   Z� ��50   t   Z� n� 53   
   -	   5        $ TopLeftThumb��53      $��20��6&   $��20��61   $ 0Q�6=    . $
��Left=�54      $	��Top=�6+   6C   n� 55   
   -
   5        $ TopRightThumb��55      $��20��6'   $��20��62   
������ $��2��6W   $ 0Q�6g    . $��Right=�56      $	��Top=�6,   6D   n� 57   
   -   5        $ BottomLeftThumb��57      $��20��6)   $��20��64   
������ $��2��6Y   $ 0Q�6f    . $
��Left=�58      $��Bottom=�6+   6F   n� 59   
   -   5        $ BottomRightThumb��59      $��20��6*   $��20��65   
������ $��2��6Z   $��2��6j   $ 0Q�6w    . $��Right=�5:      $��Bottom=�6,   6G   �� 5<   
   -   5        $��Settings��5<   A   ���	#FFC7C7C7$	��150��6(   $	��150��64   $y�Hidden=�6Q   $��2��6e   ._�5>      ��  �� Orientation$ Vertical=�6   .+�5?      �� -   5        $��	PickMedia��5?      .��6B   *(Выбрать медиа элемент6Y   �� 5@      -   5        $
��Copy��5@      .��68   Скопировать6E   �� 5A      -   5        $��HistoryCheck��5A      .��6H   64Просмотр истории добавлений6e   �� 5B      -   5        $��
ChangeSize��5B      .��6D   Изменить размер6U   �� 5C      -   5        $��Close��5C      .��6:   Закрыть6C   5D      5E      5F      5G      