   M S B A M L   `   `   `  ����F  BWPFJlizBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFJlizBankWPFJlizBank  5   	        WPFJlizBank.CustomerWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WPFJlizBank     5   	     � Title$  CustomerWindow��5   	   $	��500��6    $
��1000��6-    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5       �� 
TargetType �� Property �� Value��%[   '��       �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   6E   5      5      .��5      � .+�5   
   �� ���#4DFFFF �� CornerRadius$ 10��6&   $��20q�68   ._�5   
   �� $��10q�6   .��5      �� -   5        $��CustomerInfoList��5       �� View 5      �� 5      .P�5      �� $��客戶代碼��6-    �� Width$
 Auto��6;   	 �� DisplayMemberBinding	 6J   �� 
 �� Path$
 
CustomerId�6~   �� 5      $��姓名��6-   $
 Auto��69   	 6H   �� $
 UserName�6y   �� 5      $��電話��6-   $
 Auto��69   	 6H   �� $
 Phone�6v   �� 5      $��手機��6-   $
 Auto��69   	 6H   �� $
 Mobile�6w   �� 5      $��地址��6-   $
 Auto��69   	 6H   �� $
 Address�6x   �� 5      $��Email��6-   $
 Auto��6<   	 6K   �� $
 Email�6y   �� 5      $��建立時間��6-   $
 Auto��6;   	 6J   �� $
 InitDate�6{   �� 5      $��修改時間��6-   $
 Auto��6;   	 6J   �� $
 
ModifyDate�6}   5       5"      5#      5$      5%      5&      