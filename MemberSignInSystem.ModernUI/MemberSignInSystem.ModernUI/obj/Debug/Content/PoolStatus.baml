   M S B A M L   `   `   `  ����V  RMemberSignInSystem.ModernUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MemberSignInSystem.ModernUI.ViewModels&MemberSignInSystem.ModernUI.ViewModels  5      m;clr-namespace:MemberSignInSystem.ModernUI.Content.Behaviors-MemberSignInSystem.ModernUI.Content.Behaviors  5      4    .MemberSignInSystem.ModernUI.Content.PoolStatus   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dvms4clr-namespace:MemberSignInSystem.ModernUI.ViewModels     5      N JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5      K	behaviors;clr-namespace:MemberSignInSystem.ModernUI.Content.Behaviors  5        � 	Resources  5
       �� 
TargetType �� Property �� Value �� BasedOn   PoolHeadingSpacer  
WindowText � Color � Offset �� Path  PoolVarTextBoxTemplate �� 
TargetType	 ��Name
 �� Padding J� SnapsToDevicePixels ��Name �� ClearTypeHint �� Triggers P� Property P� Value  TransparentBrush  InputBackground  InputBorder  InputTextDisabled �� 
TargetName 
 Accent ]� 
Conditions �� Property �� Value  InputBackgroundHover 	 InputBorderHover 
 InputTextHover  PoolStatusDataTemplate �� Orientation � ColumnDefinitions  Heading2 �� 
IsReadOnly �� RelativeSource �� Mode �� AncestorType�� 5   
   %�  '��       '�� M     7��'�� �     7��'	� �     7	�&	  :    &	 P    &	 �    7  �� 5      " ��6   .�5      ��  � ��$ 0,0,0,4q�6+   5      �� 5      " ��6   8   .�5      ��  � ��$ Center=�66   5      �� 5      " ��6   8   .�5      ��  � ��$	 0,3q�6,   5      �� 5      " 	�6   8   .�5      ��  � ��$	 0,3q�6,   5      �� 5      " ��6.   .�5      ��  � ��$ 1��6+   �� 5       �@��Stretch$
 Fill=�6,   �� 5       � ��$ Center=�66   �� 5       � ��$ 1��64   �� 5       �@�DataContext# �  60   �� 5       � �� 5       �� 5!      .��5"      � $ Transparent��6+   $	 0.0Q�6?   � 5#      $
 0.15Q�6H    6+   �� $ Color�6H   � 5$      $
 0.85Q�6H    6+   �� $ Color�6H   � 5%      $ Transparent��6+   $	 1.0Q�6?   5&      5(      5)      �� 5+      " ��6=   .R�5,      �� $	 Bd��6   #��z��5-      #��z��5.      #��z��5/      #
 z��50       . ._�52      �� $ PART_ContentHost��6#   $ Enabled=�6=   # z 53   '   54       55      P� 56       �@��
IsReadOnly . . �57      �� # �  67    � ���� 58      # �  68    � ���� 59       � |� .  66   �� 5:       �@�Cursor$ Arrow��63   5;      P� 5<       �@��
IsReadOnly .  . �5=      �� # �  67    � ���� 5>      # �  68    � ���� 5?       � |� . 66   �� 5@       �@�Cursor$ IBeam��63   5A      P� 5C       � {� .  . �5D      �� # �  67    � ��5E      P� 5F       �@J�	IsFocused . . �5G      �� $ Bd��6!   # �  6H    � ��5H      ]� 5I       5J      �� 5K       �@��
IsReadOnly .  6>   �� 5L       � {� . 6=   �� 5M       �@J�IsMouseOver . 6?   ./�5O      �� # �  67    � ���� 5P      # � 	 68    � ���� 5Q      # � 
 67    � ��5R      5T      �� 5V      .R�5W      �� $��0,7,0,7q�6   $ Vertical=�6.   .+�5X      � $
��Auto��6   $��0,0,0,5q�6(    5Y      �� 5Z      $
��0.5* �6/   �� 5[      $
��Auto �6/   �� 5\      $
��0.5* �6/   .+�5^      �� $��0��6$   #���   64   �� 5_      $��1��6$   $��Center=�64   $��Center=�6O   8��  $��5,0,5,0q�5`   `   ��6$   �� *
PoolName+6`   �� 5a      $��2��6$   #���   64   5b      �� 5c      $��Stretch=�6D   ��6#   �� *
PoolVars+��5d      �� 5e      .R�5f   "   � $��0,2,0,2q�6'    5g   &   �� 5h   *   $
��0.5* �6;   �� 5i   *   $
��0.5* �6;   .+�5k   &   �� $��0��6,   $
��10,0q�6<   $
��Bold"�6c   $��Right=�6u   ��6J   �� *Name+6u   �� 5l   &   $��PoolVarValueTextBox��6.   $��1��6I   $��0,0,10,0q�6Y   #���  5n   .   $
��Left=�6b   $��30��6}   ��5l   k   �� *Value+ 5m   .   �� $! DataContext.IsNotInEditMode� �� $ FindAncestor=�$ UserControlL�5n   }   5y   #   5z      5|      5}      5~      5       � DataContext 5�      @  :MemberSignInSystem.ModernUI.ViewModels.PoolStatusViewModel  5�   
   .��5�      �� .��5�   
   �  � RowDefinitions 5�      �� 5�      $
��Auto �6    �� 5�      $��* �6    .+�5�      �� $��0��6   $ 
Horizontal=�6&   $��Center=�6?   $��Center=�6\   y�5�      �� *IsOpenClosedVisible+.+�5�      	� $
��Open��6    d� 	IsChecked 6-   �� *
IsPoolOpen+	� 5�      $��Closed��6   $��8,0,0,0q�6/    6@   �� *IsPoolClosed+�� 5�      $��8,0,0,0q�6>    �� DisplayMemberPath$ OptionString��5�       �� SelectedValuePath$ ClosureDuration��6<   $��46��5�      {�5�      �� *IsPoolClosed+��5�      �� *PoolClosureDurationOptions+  �� SelectedValue  5�      �� *SelectedPoolClosureDuration+5�      �� 5�      $��minutes��6   $��8,0,0,0q�6+   $��14(�6<   $��Center=�6J   {�5�      �� *IsPoolClosed+5�      �� 5�      $��2��6   $��Center=�6*   y�6E   �� *IsPoolClosedSignVisible+.+�5�      �� $��POOL CLOSED��6   $��36(�6/   
���  ��$��	ExtraBold"�6N   $��Center=�5�      $��Center=�69   �� 5�      $��36(�6C   
���  ��$��	ExtraBold"�6b   $��Center=�5�      $��Center=�69   ��5�      �� *PoolClosedTimerString+y�5�      �� *IsTimerVisible+5�      �� 5�      $��0��6   $
��Wrap=�6%   $��14(�69   ! �� TextAlignment$
! Left=�6G   $��Center=�6\   $��Center=�5�      $
��Bold"�63   ��6F   �� *TimeOfLastUpdateString+y�5�      �� *IsNotInEditMode+   BooleanToVisibilityConverter" �� 	Converter#" [ 7  1FirstFloor.ModernUI.Windows.Controls.ModernButton  5�      -   5        $��RefreshWeatherButton��5�   A   $��0��5�      $��Right=�5�      $	��Top=�67   '  !System.Windows.Shell.WindowChrome#   IsHitTestVisibleInChrome# . $   IconData$ 5�   ,   �� *
IconData+��5�      �� *ToggleEditModeCommand+% � ToolTip% 5�      �� *EditButtonToolTip+5�      �� 5�        PoolStatuses#���  6   #���  6H   $��1��5�      5�      5�      5�      