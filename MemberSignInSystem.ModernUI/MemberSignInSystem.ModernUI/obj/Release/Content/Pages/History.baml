   M S B A M L   `   `   `  ���� V  RMemberSignInSystem.ModernUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MemberSignInSystem.ModernUI.ViewModels&MemberSignInSystem.ModernUI.ViewModels  7    1MemberSignInSystem.ModernUI.Content.Pages.History   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dvms4clr-namespace:MemberSignInSystem.ModernUI.ViewModels     -     >�Name$  HistoryUserControl�� � 	Resources    hcs �� 
TargetType �� BasedOn �� Property �� Value��%8   &	        7���� " ��8   .���  � ��$
 Left=� � DataContext =  7MemberSignInSystem.ModernUI.ViewModels.HistoryViewModel  .��� $
��Auto�� � ColumnDefinitions �� $
��Auto ���  � RowDefinitions �� $
��Auto ��� $��* �.+�(  "System.Windows.Controls.DatePicker  -   $��HistoryDisplayDatePicker��	   SelectedDate	 �� *DisplayDate+
   BlackoutDates
 �� -   $��loginHistoryList��$��1������ *DisplayedHistory+ �� View �� .P��� $
��Date�� �� DisplayMemberBinding �� *Date+�� $
��Time�� �� *Time+�� $��Number�� �� *Id+�� $��
FamilyName�� �� *
FamilyName+