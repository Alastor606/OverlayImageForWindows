   M S B A M L   `   `   `  ����Q  MOverlayImageForWindows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:OverlayImageForWindows.ViewsOverlayImageForWindows.Views  5   	   1    +OverlayImageForWindows.Views.GetMediaWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:OverlayImageForWindows.Views     5   	     � Title$  GetMediaWindow��5   	   $	��450��6    $	��800��6-    � 
ResizeMode$ NoResize=�69   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   6*   �� 5      6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��Images��5      $	��150��6#   $��25��6/   $��150,0q�6;   {�.  .��6q   Изображения6~   �� 5      -   5        $��Videos��5      $	��150��6#   $��25��6/   $
��50,0q�6;   .��6^   
Видео6e   5      �� 5   
   -   5        $��ImageSW��5   l   $
��Auto=�6   $
��Auto=�6:   $��1��6_   .��5      A� -   5        $��	ImageGrid��5   #    A� Rows$ 2��6   5      5      �� 5   
   -   5        $��VideoSW��5   l   $
��Auto=�6   $
��Auto=�6:   $��1��6_   .��5      A� -   5        $��	VideoGrid��5   #   $ 2��6   5      5      5      5      