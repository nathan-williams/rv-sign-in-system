   M S B A M L   `   `   `  ����N  JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FirstFloor.ModernUIFirstFloor.ModernUI  5      [2clr-namespace:FirstFloor.ModernUI.Windows.Controls$FirstFloor.ModernUI.Windows.Controls  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source7    1FirstFloor.ModernUI.Windows.Controls.ModernWindow �� 
TargetType �� Property   WindowBackgroundContent �� Value  DefaultFontFamily  DefaultFontSize  
WindowText&   System.Windows.Media.TextOptions �� 
TargetType ��Name%  System.Windows.SystemParameters$   WindowNonClientFrameThickness �� Source  WindowBackgroundColor	 �� Color
 � 	Resources  Key �� 
TargetName �� TargetProperty	 �� To g� Duration  WindowBorder �Name � RowDefinitions    BackgroundContent 
 Accent  ModernWindowTitle    IsTitleVisible � DataContext 	 ToUpperConverter �� 	Converter  
 BooleanToVisibilityConverter �� 	Converter � LayoutTransform �� Angle    LogoData  NullToVisibilityConverter �� ConverterParameter �� Stretch �� Orientation'  !System.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome#   FirstFloor.ModernUI.Resources   Minimize � ToolTip  SystemButton#  System.Windows.SystemCommands   MinimizeWindowCommand  �� RelativeSource! �� Mode" �� AncestorType# ��Name$   Restore%   RestoreWindowCommand& � UseLayoutRounding'   Maximize(   MaximizeWindowCommand)   Close*   CloseWindowCommand+    
TitleLinks, ��Name	- �� X1	. �� Y1	/ �� X2	0 �� Y2  SeparatorBackground  SystemButtonLink1 �� ElementName2 �� Triggers3 �� Binding4 �� Value5 �� 
TargetName5   /FirstFloor.ModernUI.Windows.Controls.ModernMenu6    MenuLinkGroups7   
LinkGroups8   SelectedSource9 �� Mode7   1FirstFloor.ModernUI.Windows.Controls.ModernButton:   IconData6   0FirstFloor.ModernUI.Windows.Controls.ModernFrame;  Name<    ContentLoader=   ContentLoader>   Source? F�Name  WindowBorderActive@ �� TriggersA P� PropertyB P� ValueC ]� 
ConditionsD �� PropertyE �� ValueF   CornerRadiusG   GlassFrameThicknessH   UseAeroCaptionButtonsI   NonClientFrameEdges�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      .modernui!clr-namespace:FirstFloor.ModernUI  5      ?controls2clr-namespace:FirstFloor.ModernUI.Windows.Controls  5        5      �� 5   
   $7 1/FirstFloor.ModernUI;component/Assets/Button.xaml?�6   �� 5   
   $; 5/FirstFloor.ModernUI;component/Assets/Converters.xaml?�6   %~  '         7 	 7 
 7  7  7  7  7  7  7 
 �� 5      "   6   .�5   
   ��  �@  BackgroundContent# �   6.   �� 5   
    � ��# �  6'   �� 5   
    � ��# �  6%   �� 5   
    � ��# �  6'   �� 5   
    � ��$	 800��6"   �� 5   
    � ��$	 640��6#   �� 5   
    � ��$	 320��6%   �� 5   
    � ��$	 320��6&   �� 5   
    �@�
ResizeMode$ CanResizeWithGrip=�6'   �� 5   
    �@�UseLayoutRounding . 6.   �� 5   
    �@ TextFormattingMode$ Display=�6;   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� $ WindowBorder��6   ��63   �� # Z ��5      �� 5      WindowBorderBackground��#	 �  6N   
 5      ���� 5       BackgroundAnimation .��5!   "   �� $ WindowBorderBackground��61   $ Color�6`   # �  6�   $ 0:0:.6D�6�   5"      5#      ._�5%      � .+�5&      �� #���  6%   $��1q�6R   ._�5'   "   �� .u�5(   &   � $ 
LayoutRoot��6+    5)   *   �� 5*   .   $��36 �6<   �� 5+   .   $
��Auto �6<   �� 5,   .   $
��Auto �6<   �� 5-   .   $��* �6<   �� 5.   .   $��15 �6<   .+�52   *   �� $��5��69   #��z 6J   �� 55   *   $��5��61   #���  56   1   $z�1,0,0,1,0,32W�57   1   $��14��58   1   $��53��6?   $
��Left=�59   1   $	��Top=�5:   1   ._�5;   .   �� 
�������$	��0,8q�5=   8   #���  5>   8   $��Center=�5?   8   # z 5@   8   ��5;   8   �� *Title+8   y�5A   8   �� 8   5B   2   �� 5C   6   $	 -90Q�6F   5E   /   5F   +   �� 5I   *   #���  61   $��36��6W   $��36��6b   $��Right=�6n   $	��Top=�6�   $��0,0,76,0q�6�   # z 5J   1   y�5K   1   �� 8  $ inverse��._�5L   .   F� $
 Fill=�6D   
�������$��24��6`   $��24��6k   $��Center=�6w   $��Center=�6�   ��63   �� 6�   5M   +   �� 5P   *   $ 
Horizontal=�65   $��Right=�6N   $	��Top=�6j   $��0,8,6,0q�6�    . .+�5Q   .   �� # Z 6�   8�� ��65   �� # Z ��5R   2   � 5S   6   $��13��6;   $��12��6F   $z�1,0,0,1,0,1W�6R   .+�5T   :   F� ����    ��[ � z ��[ 	$��8��6R   $��7��6\   $��Center=�6g   $��Center=�6�   $��2��5U   �   ��6?   �� *
Foreground+  �� $! FindAncestor=�$" ButtonL�6�   5V   7   5X   /   � 5Y   .   $��1,0,1,0q�63   .+�5Z   2   �� $# Restore��69   # Z$ 6�   8�� $y�	Collapsed=�6�   ��6J   �� # Z% ��5[   6   � 5\   :   $��13��6?   $��12��6J   & . $z�1,0,0,1,.5,.5W�6o   .+�5]   >   F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��6y   $��8��6�   $��Center=�6�   $��Center=�6�   $��1��5^   �   ��6C   �� *
Foreground+  �� $! FindAncestor=�$" ButtonL�6�   5_   ;   5a   3   �� 5b   2   $# Maximize��69   # Z' 6�   8�� ��6K   �� # Z( ��5c   6   � 5d   :   $��13��6?   $��12��6J   .+�5e   >   F� +����    @B �@T� @B �@T�  z �     z 	$��9��6`   $��8��6j   $��Center=�6u   $��Center=�6�   $��2��5f   �   ��6C   �� *
Foreground+  �� $! FindAncestor=�$" ButtonL�6�   5g   ;   5i   3   5j   /   �� 5k   .   # Z) 6}   8�� ��65   �� # Z* ��5l   2   � 5m   6   $��13��6;   $��12��6F   $z�1,0,0,1,0,1W�6R   .+�5n   :   F� +����        � z ��j � z     �    ��j 	$��8��6\   $��7��6f   $��Center=�6q   $��Center=�6�   $	��1.5��5o   �   ��6?   �� *
Foreground+  �� $! FindAncestor=�$" ButtonL�6�   5p   7   5r   /   5s   +   �� 5v   *   #��z+ 67   $��Right=�6b   $	��Top=�6~   $��	0,6,180,0q�6�    . ��5w   .   �� 5x   2   .R�5y   6   �� $ 
Horizontal=�6A   5z   3   ��5|   .   �� 5}   2   .R�5~   6   �� $ 
Horizontal=�6A   .+�5   :   �� $, 	Separator��6?   $- .5��6R   $. 3��6Z   $/ .5��6a   $0 12��6i   $��7,0,5,0q�6q   #���  6�   �� 5�   :   $!��NavigationCommands.GoToPage��5�   A   8�� ��5�   A   �� *DisplayName+��5�   A   �� *Source+��5�   A   �� $1 ContentFrame��5�   A   5�   7   2 5�   6   �� 5�   :   3 6F   ��   �� *PreviousData+4 6�   Z� .a�5�   >   �� $5 	Separator��6[    � y�$ 	Collapsed=�6r   5�   ;   5�   3   5�   +     5�   *   $��1��6>   $��36,0,0,0q�5�   >   #7 z6 5�   >   8 5�   >   �� *Source+$1 ContentFrame��$9 TwoWay=�5�   >     5�   *   a: ��@����O�@������p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O�@����O$#��NavigationCommands.BrowseBack��5�   @   $
 back��5�   @   $��2��5�   @   $
��Left=�6Q   $	��Top=�6l   $��	34,24,0,0q�6�    . ��5�   @   �� $1 ContentFrame��5�   +     5�   *   $; ContentFrame��6?   $��3��6U   $��2��6b   $��
36,8,16,16q�6s   #= z< 6�   > 6�   �� *ContentSource+  �� *TemplatedParent+$9 TwoWay=�6�   � 5�   *   $��4��6/   .+�5�   .   F� $? 
ResizeGrip��63   $y�	Collapsed=�6G   $��12��6^   $��12��6i   $��1q�6u   $��Right=�6�   #���  5�   3   $��1��5�   3   $
 None=�5�   3   �����@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- 5�   3   5�   +   5�   '   5�   #   5�      �� 5�      #���  6%   $��1q�6X   y�6l   �� *
IsActive+  �� *FindAncestor+"" �8  5�      5�      @ 5�      P� 5�      A �@�WindowState$B 	Maximized=�69   . �5�      �� $5 Maximize��6%    � y�$ 	Collapsed=�6Q   �� 5�      $5 Restore��6%    � y�$ Visible=�6P   �� 5�      $5 
LayoutRoot��6%    � ��$ 7q�6O   5�      P� 5�      A �@�WindowState$B Normal=�69   . �5�      �� $5 Maximize��6%    � y�$ Visible=�6Q   �� 5�      $5 Restore��6%    � y�$ 	Collapsed=�6P   5�      ]� 5�      C 5�      �� 5�   "   D �@�
ResizeMode$E CanResizeWithGrip=�6B   �� 5�   "   D �@�WindowState$E Normal=�6C   ./�5�      �� $5 
ResizeGrip��6%    � y�$ Visible=�6S   5�      5�      5�      �� 5�   
    �@ WindowChrome 5�        5�      $F 0��6   $G 1q�60   H .  $
I None=�6f   5�      5�      5�      