   M S B A M L   `   `   `  ���� V  RMemberSignInSystem.ModernUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MemberSignInSystem.ModernUI.ViewModels&MemberSignInSystem.ModernUI.ViewModels  m;clr-namespace:MemberSignInSystem.ModernUI.Content.Behaviors-MemberSignInSystem.ModernUI.Content.Behaviors  4    .MemberSignInSystem.ModernUI.Content.PoolStatus   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dvms4clr-namespace:MemberSignInSystem.ModernUI.ViewModels     N JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  K	behaviors;clr-namespace:MemberSignInSystem.ModernUI.Content.Behaviors    � 	Resources   �� 
TargetType �� Property �� Value �� BasedOn   PoolHeadingSpacer  
WindowText � Color � Offset �� Path  PoolVarTextBoxTemplate �� 
TargetType	 ��Name
 �� Padding J� SnapsToDevicePixels ��Name �� ClearTypeHint �� Triggers P� Property P� Value  TransparentBrush  InputBackground  InputBorder  InputTextDisabled �� 
TargetName 
 Accent ]� 
Conditions �� Property �� Value  InputBackgroundHover 	 InputBorderHover 
 InputTextHover  PoolStatusDataTemplate �� Orientation � ColumnDefinitions  Heading2 �� 
IsReadOnly �� RelativeSource �� Mode �� AncestorType�� %  '��       '�� (     7��'�� T     7��'	� }     7	�&	  �     &	 �    &	 R    7  �� " ��.���  � ��$ 0,0,0,4q��� " ��8   .���  � ��$ Center=��� " ��8   .���  � ��$	 0,3q��� " 	�8   .���  � ��$	 0,3q��� " ��.���  � ��$ 1����  �@��Stretch$
 Fill=���  � ��$ Center=���  � ��$ 1����  �@�DataContext# �  ��  � �� �� .��� $ Transparent��$	 0.0Q�� $
 0.15Q� �� $ Color�� $
 0.85Q� �� $ Color�� $ Transparent��$	 1.0Q��� " ��.R��� $	 Bd��#��z��#��z��#��z��#
 z�� . ._��� $ PART_ContentHost��$ Enabled=�# z  P�  �@��
IsReadOnly . . ��� # �   � ���� # �   � ����  � |� .  ��  �@�Cursor$ Arrow��P�  �@��
IsReadOnly .  . ��� # �   � ���� # �   � ����  � |� . ��  �@�Cursor$ IBeam��P�  � {� .  . ��� # �   � ��P�  �@J�	IsFocused . . ��� $ Bd��# �   � ��]�  ��  �@��
IsReadOnly .  ��  � {� . ��  �@J�IsMouseOver . ./��� # �   � ���� # � 	  � ���� # � 
  � ���� .R��� $��0,7,0,7q�$ Vertical=�.+�� $
��Auto��$��0,0,0,5q� �� $
��0.5* ��� $
��Auto ��� $
��0.5* �.+��� $��0��#���   �� $��1��$��Center=�$��Center=�8��  $��5,0,5,0q����� *
PoolName+�� $��2��#���   �� $��Stretch=����� *
PoolVars+���� .R�� $��0,2,0,2q� �� $
��0.5* ��� $
��0.5* �.+��� $��0��$
��10,0q�$
��Bold"�$��Right=����� *Name+�� $��PoolVarValueTextBox��$��1��$��0,0,10,0q�#���  $
��Left=�$��30������ *Value+ �� $! DataContext.IsNotInEditMode� �� $ FindAncestor=�$ UserControlL� � DataContext @  :MemberSignInSystem.ModernUI.ViewModels.PoolStatusViewModel  .���� .���  � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��$ 
Horizontal=�$��Center=�$��Center=�y��� *IsOpenClosedVisible+.+�	� $
��Open�� d� 	IsChecked �� *
IsPoolOpen+	� $��Closed��$��8,0,0,0q� �� *IsPoolClosed+�� $��8,0,0,0q� �� DisplayMemberPath$ OptionString�� �� SelectedValuePath$ ClosureDuration��$��46��{��� *IsPoolClosed+���� *PoolClosureDurationOptions+  �� SelectedValue  �� *SelectedPoolClosureDuration+�� $��minutes��$��8,0,0,0q�$��14(�$��Center=�{��� *IsPoolClosed+�� $��2��$��Center=�y��� *IsPoolClosedSignVisible+.+��� $��POOL CLOSED��$��36(�
���  ��$��	ExtraBold"�$��Center=�$��Center=��� $��36(�
���  ��$��	ExtraBold"�$��Center=�$��Center=����� *PoolClosedTimerString+y��� *IsTimerVisible+�� $��0��$
��Wrap=�$��14(�! �� TextAlignment$
! Left=�$��Center=�$��Center=�$
��Bold"����� *TimeOfLastUpdateString+y��� *IsNotInEditMode+   BooleanToVisibilityConverter" �� 	Converter#" [ 7  1FirstFloor.ModernUI.Windows.Controls.ModernButton  -   $��RefreshWeatherButton��$��0��$��Right=�$	��Top=�'  !System.Windows.Shell.WindowChrome#   IsHitTestVisibleInChrome# . $   IconData$ �� *
IconData+���� *ToggleEditModeCommand+% � ToolTip% �� *EditButtonToolTip+��   PoolStatuses#���  #���  $��1��