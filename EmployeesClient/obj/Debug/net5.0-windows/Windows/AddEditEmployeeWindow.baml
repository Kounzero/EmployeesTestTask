   M S B A M L   `   `   `  ����J  FEmployeesClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:EmployeesClient.WindowsEmployeesClient.Windows  5   	   3    -EmployeesClient.Windows.AddEditEmployeeWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:EmployeesClient.Windows     5   	   -   5          � Title$B  <Добавление/изменение сотрудника��5   	   $	��450��5	   	   $	��400��5
   	   $	��350��5   	   $	��350��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��ФИО:��6   $��Center=�5      $��Right=�5      $��5q�5       $��0��5!      �� 5"   
   $ ��Дата рождения:��6   $��Center=�5#      $��Right=�5$      $��5q�5%      $��1��5&      �� 5'   
   $��Пол:��6   $��Center=�5(      $��Right=�5)      $��5q�5*      $��2��5+      �� 5,   
   $��Должность:��6   $��Center=�5-      $��Right=�5.      $��5q�5/      $��3��50      �� 51   
   $*��$Водительские права:��6   $��Center=�52      $��Right=�53      $��5q�54      $��4��55      �� 56   
   $!��Подразделение:��6   $��Center=�57      $��Right=�58      $��5q�59      $��5��5:      �� 5<   
   -   5         ��Name$ FullNameTextBox��5<      $��1��5=      $��5q�5>      $��Center=�5?      ��5@      �� *
FullName+(  "System.Windows.Controls.DatePicker  5A   
   -   5          Name$ BirthDatePicker��5A      $��1��5B      $��1��5C      $��Center=�5D      $��5q�5E         SelectedDate 5F      �� *	BirthDate+�� 5G   
   -   5         ��Name$ GendersCombobox��5G      $��1��5H      $��2��5I      $��5q�5J      $��Center=�5K       �� SelectedValuePath$ Id��5M       �� DisplayMemberPath$ Title��5N      	 �� SelectedValue	 5L      �� *
GenderId+5N      �� 5O   
   -   5        $ PositionsCombobox��5O      $��1��5P      $��3��5Q      $��5q�5R      $��Center=�5S      $ Id��5U      $ Title��5V      	 5T      �� *
PositionId+5V      �� 5W   
   -   5        
 ��Name$
 HasDrivindLicenseCheckBox��5W      $��Center=�5X      $��1��5Z      $��4��5[      $��5q�5\       d� 	IsChecked 5Y      �� *HasDrivingLicense+5\      �� 5]   
   -   5        $ SubdivisionsCombobox��5]      $��1��5^      $��5��5_      $��5q�5`      $��Center=�5a      $ Id��5c      $ Title��5d      	 5b      �� *SubdivisionId+5d      �� 5f   
   -   5         ��Name$ BtnOk��5f      $��Сохранить��5g      $��1��5i      $��6��5j      $��5q�5k      $��Center=�5l      �� 5m   
   -	   5        $ 	BtnCancel��5m      $��Отмена��5n      $��6��5p      $��5q�5q      $��Center=�5r      5s      5t      