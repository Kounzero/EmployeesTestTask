   M S B A M L   `   `   `  ����J  FEmployeesClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:EmployeesClient.WindowsEmployeesClient.Windows  5   	   -    'EmployeesClient.Windows.EmployeesWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:EmployeesClient.Windows     5   	   -   5          � Title$8  2Подразделения и сотрудники��5   	   $	��450��5	   	   $	��800��5
   	   $	��300��5   	   $	��700��5   	    � 	Resources 5         SubdivisionItemTemplate �� Orientation � ToolTip ��Name��%�  &	        �� 5   
   .R�5      �� $ 
Horizontal=�6   ��5      �� *MarginForDisplay+ 5      �� *Description+.+�5      �� $ BtnOpenChildren��6   -   5        $��Center=�5      $��15��5      $��15��5      $��5 0 5 0q�5      
������ 
������ $��0q�5      ��5      �� *BtnDisplayContent+y�5      �� *ShowedVisibility+5      �� 5      $��Center=�5      ��5      �� *Title+5      5      5       5!      .��5"      �  � ColumnDefinitions 5#   
   �� 5$      �� 5%      $��2* �6    � RowDefinitions 5'   
   �� 5(      $��80 �6   �� 5)      .+�5,   
   �� $ ��Подразделения��6   $
��Left=�5-      $��Center=�5.      $��10q�5/      .��50      �� $ 
Horizontal=�6   $��Center=�51      .+�52      �� -   5        $ BtnAddSubdivision��52      $��Добавить��53      $��10 5 5 5q�55      �� 56      -   5        $ BtnEditSubdivision��56      $��Изменить��57      $��5q�59      �� 5:      -   5        $ BtnDeleteSubdivision��5:      $��Удалить��5;      $��5q�5=      5>      5?      �� 5A   
   $��Сотрудники��6   $
��Left=�5B      $��Center=�5C      $��10q�5D      $��1��5E      .��5F      �� $ 
Horizontal=�6   $��Center=�5G      .+�5H      �� -   5        $ BtnAddEmployee��5H      $��Добавить��5I      $��10 5 5 5q�5K      �� 5L      -   5        $ BtnEditEmployee��5L      $��Изменить��5M      $��5q�5O      �� 5P      -   5        $ BtnDeleteEmployee��5P      $��Удалить��5Q      $��5q�5S      5T      5U      �� 5W   
   -	   5         ��Name$ SubdivisionsListView��5W      $��1��5X      $��10q�5Y       �� SelectionMode$ Single=�5Z      #��[  5\      &   System.Windows.Controls.DataGrid  5^   
   -
   5        	  Name$	 EmployeesDataGrid��5^      $��1��5_      $��1��5`      
   SelectionMode$
 Single=�5a      $��10q�5b         AutoGenerateColumns$ False��5d         
IsReadOnly$
 True��5e         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Columns 5f      0  *System.Windows.Controls.DataGridTextColumn  5g      ,  &System.Windows.Controls.DataGridColumn   Header$ ФИО��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5h   %   �� *
FullName+  5i      $ Дата рождения��6%    5j   %   �� *
ShowedDate+  5k      $ Пол��6%    5l   %   �� *GenderTitle+  5m      $ Должность��6%    5n   %   �� *PositionTitle+  5o      $ Есть права?��6%    5p   %   �� *ShowedHasDrivingLicense+  5q      $  Подразделение��6%    5r   %   �� *SubdivisionTitle+5t      5w      5x      