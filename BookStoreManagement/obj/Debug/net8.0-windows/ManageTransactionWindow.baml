   M S B A M L   `   `   `  ����N  JBookStoreManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BookStoreManagementBookStoreManagement  5   	   1    +BookStoreManagement.ManageTransactionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:BookStoreManagement     5   	     � Title$  ManageTransactionWindow��5   	   $	��450��6)   $	��850��66   .��5
      � !  System.Windows.SystemColors   MenuBrushKey#���  6   .+�5   
   �� 
���   �$��1q�6%   $��Center=�69   $	��366��6V   $��0,28,0,0q�6c   $	��Top=�6u   $	��786��6�      HighlightTextBrushKey#���  6�   �� 5   
   -   5         ��Name$
 ISBN��5      $
��Left=�6"   $��25��6=   $��	75,75,0,0q�6I   $
��Wrap=�6\   $
��ISBN��6p   $	��Top=�6|   $��94��6�   �� 5   
   -   5         ��Name$ ISBN_txt��5      $
��Left=�6$   $��25��6?   $��
181,72,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��371��6�   �� 5   
   -   5        $ 
book_Title��5      $
��Left=�6(   $��25��6C   $��
75,106,0,0q�6O   $
��Wrap=�6c   $��
Book Title��6w   $	��Top=�6�   $��94��6�   �� 5   
   -   5        $ book_Title_txt��5      $
��Left=�6*   $��25��6E   $��181,103,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��371��6�   �� 5   
   -   5        $ price��5      $
��Left=�6#   $��25��6>   $��
76,138,0,0q�6J   $
��Wrap=�6^   $��Price��6r   $	��Top=�6   $��94��6�   �� 5   
   -   5        $ price_double��5      $
��Left=�6(   $��25��6C   $��182,138,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��326��6�   �� 5   
   -   5        $ currency��5      $
��Left=�6&   $��25��6A   $��528,139,0,0q�6M   $
��Wrap=�6b   $	��THB��6v   $	��Top=�6�   $��29��6�   �� 5   
   -   5         ��Name$ search��5      $��Search��6!   $
��Left=�62   $��31��6M   $��442,178,0,0q�6Y   $	��Top=�6n   $	��110��6�   �� 5   
   -	   5        $ customer_ID��5      $
��Left=�6)   $��25��6D   $��
75,221,0,0q�6P   $
��Wrap=�6d   $��Customer ID��6x   $	��Top=�6�   $��94��6�   �� 5   
   -
   5        $ customer_ID_txt��5      $
��Left=�6+   $��25��6F   $��181,218,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��371��6�   �� 5   
   -   5        $ quatity��5      $
��Left=�6%   $��25��6@   $��
75,252,0,0q�6L   $
��Wrap=�6`   $	��Qty��6t   $	��Top=�6   $��94��6�   �� 5   
   -   5        $ quatity_int��5      $
��Left=�6'   $��25��6B   $��181,252,0,0q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��326��6�   �� 5   
   -   5        $ Ea��5      $
��Left=�6    $��26��6;   $��523,255,0,0q�6G   $
��Wrap=�6\   $��Ea��6p   $	��Top=�6z   $��29��6�   �� 5   
   -   5        $ 	check_Out��5      $��	Check Out��6$   $
��Left=�68   $��31��6S   $��321,292,0,0q�6_   $	��Top=�6t   $	��110��6�   �� 5   
   -   5        $
 back��5      $
��Back��6   $
��Left=�6.   $��31��6I   $	��Top=�6U   $	��110��6m   $��442,292,0,0q�6y   �� 5   
   -   5        $	 add��5      $	��Add��6   $
��Left=�6,   $��31��6G   $	��Top=�6S   $	��110��6k   $��321,178,0,0q�6w   5      5       