   M S B A M L   `   `   `  ����C  ?CarShare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CarShare.ViewCarShare.View  5      7 clr-namespace:CarShare.ViewModelCarShare.ViewModel  5      "    CarShare.View.ChangePassword   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:CarShare.View     5      /vm clr-namespace:CarShare.ViewModel     5        � 	Resources  5      "  CarShare.ViewModel.ViewModel   mainViewModel��%   &	          5   
   6   5      .��5      �  � DataContext# [  6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   .+�5   
   �� $��Old Password��6   $��Center=�6'   $��1��6D   $��10q�6Q   �� 5   
   -   5        $��txtOldPassword��5      $��2��6(   $��10q�65   ��6A   �� *%#changePasswordViewModel.OldPassword+�� 5   
   $��New Password��6   $��Center=�6'   $��3��6D   $��10q�6Q   �� 5   
   -   5        $��txtNewPassword��5      $��4��6(   $��10q�65   ��6A   �� *%#changePasswordViewModel.NewPassword+�� 5   
   -   5        $��btnChangePassword��5      $��Change Password��6*   $��5��6D   $��60 10q�6Q   ��5      �� *(&changePasswordViewModel.ChangePassword+5       5!      