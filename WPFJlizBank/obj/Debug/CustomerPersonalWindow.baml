   M S B A M L   `   `   `  ����F  BWPFJlizBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFJlizBankWPFJlizBank  5   	   (    "WPFJlizBank.CustomerPersonalWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WPFJlizBank     5   	     � Title$  CustomerPersonalWindow��5   	   $	��450��6(   $	��800��65    � WindowStartupLocation$ CenterScreen=�5	   	   .��5      � .+�5   
   �� .��5      �� -   5         ��Name$ BankInfoList��5      $��10q�6-    �� View 5      �� 5      .P�5      �� $��銀行帳號��6)    �� Width$
 Auto��67    �� DisplayMemberBinding 6D   ��  �� Path$ 
AccountNum�6w   �� 5      $��銀行代碼��6)   $
 Auto��67    6D   �� $ BankId�6s   �� 5      $��銀行名稱��6)   $
 Auto��67    6D   �� $ BankName�6u   �� 5      $��登入帳號��6)   $
 Auto��67    6D   �� $ LoginAccount�6y   �� 5      $��
Hash密碼��6)   $
 Auto��69    6F   �� $ HashPassword�6{   �� 5      $��帳戶餘額��6)   $
 Auto��67    6D   �� $ AccountBalance�6{   �� 5      $��警示帳戶��6)   $
 Auto��67    6D   �� $ AllertAccount�6z   5      5      5      5      5      