   M S B A M L   `   `   `  ����N  JBookStoreManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BookStoreManagementBookStoreManagement  5   	   *    $BookStoreManagement.ManageBookWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:BookStoreManagement     5   	     � Title$  ManageBookWindow��5   	   $	��600��6"   $	��850��6/    � 
ResizeMode$ NoResize=�6;   .��5
      � -   5         �Name$	 Add��5
      !  System.Windows.SystemColors   MenuBrushKey#���  6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $
��Left=�6   $��19��6/   $��	34,33,0,0q�6;   $
��Wrap=�6N   $��	Book Menu��6b   $	��Top=�6s   $��90��6�   �� 5   
   
���   �$��1q�6%   $��Center=�69   $	��258��6V   $��0,52,0,0q�6c   $	��Top=�6u   $	��786��6�      HighlightTextBrushKey#���  6�   �� 5   
   -   5         ��Name$
 ISBN��5      $
��Left=�6"   $��25��6=   $��	75,71,0,0q�6I   $
��Wrap=�6\   $
��ISBN��6p   $	��Top=�6|   $��94��6�   �� 5   
   -   5         ��Name$ ISBN_txt��5      $
��Left=�6$   $��25��6?   $��
181,68,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��479��6�   �� 5   
   -   5        $ 
book_Title��5      $
��Left=�6(   $��25��6C   $��
75,102,0,0q�6O   $
��Wrap=�6c   $��
Book Title��6w   $	��Top=�6�   $��94��6�   �� 5   
   -   5        $ book_Title_txt��5      $
��Left=�6*   $��25��6E   $��
181,99,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $	��479��6�   �� 5   
   -   5        $ book_Description��5      $
��Left=�6.   $��25��6I   $��
75,134,0,0q�6U   $
��Wrap=�6i   $��Book Description��6}   $	��Top=�6�   $��94��6�   �� 5   
   -   5        $ book_Description_txt��5      $
��Left=�60   $	��133��6K   $��181,131,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $	��479��6�   �� 5   
   -   5        $ price��5      $
��Left=�6#   $��25��6>   $��
75,273,0,0q�6J   $
��Wrap=�6^   $��Price��6r   $	��Top=�6   $��94��6�   �� 5   
   -	   5        $ book_Price_decimal��5      $
��Left=�6.   $��25��6I   $��181,273,0,0q�6U   $
 Wrap=�6j   $	��Top=�6~   $	��438��6�   �� 5   
   -
   5        	 ��Name$		 add��5      $	��Add��6   $
��Left=�6,   $��31��6G   $��
690,68,0,0q�6S   $	��Top=�6g   $	��110��6   6�   �� 5   
   -   5        $	 delete��5      $��Delete��6!   $
��Left=�62   $��30��6M   $��690,116,0,0q�6Y   $	��Top=�6n   $	��110��6�   6�   �� 5   
   -   5        $	 search��5      $��Search��6!   $
��Left=�62   $��31��6M   $��690,214,0,0q�6Y   $	��Top=�6n   $	��110��6�   6�   �� 5   
   -   5        $	 update��5      $��Update��6!   $
��Left=�62   $��32��6M   $��690,166,0,0q�6Y   $	��Top=�6n   $	��110��6�   6�   �� 5   
   -   5        $ currency��5      $
��Left=�6&   $��25��6A   $��638,274,0,0q�6M   $
��Wrap=�6b   $	��THB��6v   $	��Top=�6�   $��29��6�   �� 5   
   -   5        $
	 back��5      $
��Back��6   $
��Left=�6.   $��31��6I   $	��Top=�6U   $	��110��6m   $��690,265,0,0q�6y   6�   �� 5    
   $
��Left=�6   $��19��6/   $��
33,322,0,0q�6;   $
��Wrap=�6O   $��Book Database��6c   $	��Top=�6x   $��90��6�   �� 5!   
   -   5        
 ��Name$
 BooksListView��5!      $��32,341,32,14q�6*    �� View 5"      �� 5#      .P�5$      �� $
��ISBN��6%    �� Width$	 120��6Y    �� DisplayMemberBinding 63   �� *ISBN+6Y   �� 5%      $��Title��6%   $	 250��6[    64   �� *Title+6[   �� 5&      $��Description��6%   $	 310��6g    6:   �� *Description+6g   �� 5'      $��Price (THB)��6%   $	 100��6a    6:   �� *Price+6a   5(      5*      5+      5-      