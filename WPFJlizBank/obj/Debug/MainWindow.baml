   M S B A M L   `   `   `  ����F  BWPFJlizBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFJlizBankWPFJlizBank  5   	   I EXamlFlair.WPF, Version=1.2.13.0, Culture=neutral, PublicKeyToken=null<.clr-namespace:XamlFlair;assembly=XamlFlair.WPF	XamlFlair 5   	       WPFJlizBank.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WPFJlizBank     5   	   =xf.clr-namespace:XamlFlair;assembly=XamlFlair.WPF     5   	     � Title$  
MainWindow��5	   	   $	��500��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5       �� 
TargetType �� Property �� Value��%  '��       �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   6E   �� 5       � ��$	 280��6&   64   �� 5       � ��$ 40(�6)   66   �� 5       � ��$ Black"�6+   6;   �� 5       �@��TextAlignment$ Center=�6.   6?   5      5      .��5      �   XamlFlair.Animations   Primary 6      XamlFlair.AnimateExtension     FadeOut   BasedOn# [     Delay$
 4000Q���5   
   �� 5      	 �� ImageSource$	 /Images/EwalletBG.png��6   6?   
 � RowDefinitions
 5   
   �� 5      $��2* �6   6*   �� 5      $
��4.5* �6   6,   �� 5      $
��1.2* �6   6,   .+�5    
   �� $��0��6   $	��250��6�   $��0,5,0,0q�6�    6"       FadeInAndSlideFromBottom# [ $	 500Q���5!      �� 5"      $	 /Images/eye.png��6    �� Opacity$	 0.1Q�6;   6K   .+�5$      �� $��歡迎使用��6   $��0,15,0,0q�6$   68   �� 5%      $��行動銀行��6   6'   5&      �� 5(   
   $��1��6   $��/Images/Ewallet.png��6    6:       FadeInAndGrow# [ $	 200Q�6�   �� 5*   
   $��2��6   .+�5+      �� $��傑力士商業銀行��6    6'     # [ $	 700Q�6�   5,      5.      50      