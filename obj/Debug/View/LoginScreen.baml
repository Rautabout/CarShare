   M S B A M L   `   `   `  ����C  ?CarShare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CarShare.ViewCarShare.View  5   	   7 clr-namespace:CarShare.ViewModelCarShare.ViewModel  5   	       CarShare.View.LoginScreen   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CarShare.View     5   	   /vm clr-namespace:CarShare.ViewModel     5   	   -   5          �Name$  window��5	   	    � Title$ LoginScreen��5
   	   $	��300��6   $	��400��6*    � 	Resources 5      "  CarShare.ViewModel.ViewModel   loginVM��%   &	          5   
   6   5      .��5      �  � DataContext# [  6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   .+�5   
   �� $��	User Name��6   $��Center=�6$   $��1��6A   $��10q�6N   �� 5   
   -   5        $��txtUsername��5      $��2��6%   $��10q�62   ��6>   �� *loginViewModel.UserName+�� 5   
   $��Password��6   $��Center=�6#   $��3��6@   $��10q�6M   �� 5   
   -   5        $��txtPassword��5      $��4��6%   $��10q�62   ��6>   �� *loginViewModel.Password+�� 5   
   -   5        $��btnLogin��5      $��Login��6!   $��5��61   $��60 10q�6>   ��5      �� *loginViewModel.Login+��5      ��  �� ElementName$ window��5!      5"      