   M S B A M L   `   `   `  ����C  ?CarShare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CarShare.ViewCarShare.View  5   	       CarShare.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CarShare.View     5   	     � Title$  
MainWindow��5   	   $	��550��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   .+�5   
   �� $��1��6   $��1��6"    �� Orientation$ Vertical=�63   $
��Left=�6J   $	��290��6e   .+�5      �� $��Vehicles��6   $��Vehicles��6N   $��10q�6j   $	��100��6v   ��6(   �� *UpdateViewCommand+6v   �� 5      $��My Bids��6   $��MyBids��6M   $��10q�6g   $	��100��6s   ��6'   �� *UpdateViewCommand+6s   �� 5      $��My Vehicles��6   $��
MyVehicles��6Q   $��10q�6o   $	��100��6{   ��6+   �� *UpdateViewCommand+6{   �� 5      $��Change Password��6   $��ChangePassword��6U   $��10q�6w   $	��100��6�   ��6/   �� *UpdateViewCommand+6�   �� 5      -   5        $��Logout��5      $��10q�6=   $	��100��6I   �� 5      -   5        $
��Quit��5      $��10q�6<   $	��100��6H   �� 5       $��Manage Users��6   $��10q�6,   $	��100��68   $y�Hidden=�6D   5!      �� 5$   
   $��1��6   $��2��6&   $��2��66   ��6G   �� *SelectedViewModel+5'      5(      