   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    FocusMe   FocusMe.View.Status   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Omahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	     � Title$  FocusMe��5   	   $	��450��6   $	��650��6&    � WindowStartupLocation$ CenterScreen=�62   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Sessions left:��6   $��20(�6)   $��20q�67   $
��Left=�6C   �� 5   
   $��20q�6:   $��20(�6F   $��Center=�6T   ��6   �� *Status.SessionsNumber+6T   d� 5   
   -   5         d�Name$ PlayWavesButton��5      $��10q�60   
���   �    MetroCircleToggleButtonStyle#��[  6P   $��1��6�   $	��Top=�6�   $��Center=�6�   $��60��6�   $��60��6   d� 	IsChecked 6�   �� *IsPlayingAlpha+ �� Mode$ TwoWay=�.��5      �� $��25��6   $��25��6$   
���   � J� OpacityMask 5      !� 5       j� Stretch$
 Fill=�6"     appbar_sound_2	 !� Visual#	 �  61   5      5      �� 5   
   $��1��6   $��2��6   $	��Top=�6Q   $��Center=�6i   $��40(�6�   $��20q�6�   ��61   �� *SessionInfo+6�   �� 5   
   -   5        
 ��Name$
 
TimerLabel��5   H   $��1��6\   $��2��6i   $��Center=�6~   $��Center=�6�   $	��120(�6�   ��6   �� *%#Status.CurrentSession.SessionLength+6�   5      5      