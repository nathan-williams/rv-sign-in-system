   M S B A M L   `   `   `  ����V  RMemberSignInSystem.ModernUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MemberSignInSystem.ModernUI.ViewModels&MemberSignInSystem.ModernUI.ViewModels  5      7    1MemberSignInSystem.ModernUI.Content.Pages.History   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dvms4clr-namespace:MemberSignInSystem.ModernUI.ViewModels     5      -   5          >�Name$  HistoryUserControl��5       � 	Resources 5
         hcs �� 
TargetType �� BasedOn �� Property �� Value��%]   &	        7���� 5   
   " ��6   8   .�5      ��  � ��$
 Left=�6;   5      5       � DataContext 5      =  7MemberSignInSystem.ModernUI.ViewModels.HistoryViewModel  5   
   .��5      � $
��Auto��6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   (  "System.Windows.Controls.DatePicker  -   5        $��HistoryDisplayDatePicker��5      	   SelectedDate	 65   �� *DisplayDate+
   BlackoutDates
 5       5!      �� 5"   
   -   5        $��loginHistoryList��5"      $��1��6T   ��6+   �� *DisplayedHistory+ �� View 5#      �� 5$      .P�5%      �� $
��Date��6%    �� DisplayMemberBinding 63   �� *Date+�� 5&      $
��Time��6%    63   �� *Time+�� 5'      $��Number��6%    65   �� *Id+�� 5(      $��
FamilyName��6%    69   �� *
FamilyName+5)      5+      5,      5-      