   M S B A M L   `   `   `  ���� N  JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FirstFloor.ModernUIFirstFloor.ModernUI  [2clr-namespace:FirstFloor.ModernUI.Windows.Controls$FirstFloor.ModernUI.Windows.Controls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source7    1FirstFloor.ModernUI.Windows.Controls.ModernWindow �� 
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
ConditionsD �� PropertyE �� ValueF   CornerRadiusG   GlassFrameThicknessH   UseAeroCaptionButtonsI   NonClientFrameEdges�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     .modernui!clr-namespace:FirstFloor.ModernUI  ?controls2clr-namespace:FirstFloor.ModernUI.Windows.Controls    �� $7 1/FirstFloor.ModernUI;component/Assets/Button.xaml?��� $; 5/FirstFloor.ModernUI;component/Assets/Converters.xaml?�%�  '         7 	 7 
 7  7  7  7  7  7  7 
 �� "   .���  �@  BackgroundContent# �   ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��$	 800����  � ��$	 640����  � ��$	 320����  � ��$	 320����  �@�
ResizeMode$ CanResizeWithGrip=���  �@�UseLayoutRounding . ��  �@ TextFormattingMode$ Display=���  � �� �� "   .R��� $ WindowBorder������ # Z ���� WindowBorderBackground��#	 �  
 ���� BackgroundAnimation .���� $ WindowBorderBackground��$ Color�# �  $ 0:0:.6D�._�� .+��� #���  $��1q�._��� .u�� $ 
LayoutRoot�� �� $��36 ��� $
��Auto ��� $
��Auto ��� $��* ��� $��15 �.+��� $��5��#��z �� $��5��#���  $z�1,0,0,1,0,32W�$��14��$��53��$
��Left=�$	��Top=�._��� 
�������$	��0,8q�#���  $��Center=�# z ���� *Title+8   y��� 8   �� $	 -90Q��� #���  $��36��$��36��$��Right=�$	��Top=�$��0,0,76,0q�# z y��� 8  $ inverse��._�F� $
 Fill=�
�������$��24��$��24��$��Center=�$��Center=����� �� $ 
Horizontal=�$��Right=�$	��Top=�$��0,8,6,0q� . .+��� # Z 8�� ���� # Z ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� ����    ��[ � z ��[ 	$��8��$��7��$��Center=�$��Center=�$��2������ *
Foreground+  �� $! FindAncestor=�$" ButtonL�� $��1,0,1,0q�.+��� $# Restore��# Z$ 8�� $y�	Collapsed=����� # Z% ��� $��13��$��12��& . $z�1,0,0,1,.5,.5W�.+�F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��$��8��$��Center=�$��Center=�$��1������ *
Foreground+  �� $! FindAncestor=�$" ButtonL��� $# Maximize��# Z' 8�� ���� # Z( ��� $��13��$��12��.+�F� +����    @B �@T� @B �@T�  z �     z 	$��9��$��8��$��Center=�$��Center=�$��2������ *
Foreground+  �� $! FindAncestor=�$" ButtonL��� # Z) 8�� ���� # Z* ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� +����        � z ��j � z     �    ��j 	$��8��$��7��$��Center=�$��Center=�$	��1.5������ *
Foreground+  �� $! FindAncestor=�$" ButtonL��� #��z+ $��Right=�$	��Top=�$��	0,6,180,0q� . ���� .R��� $ 
Horizontal=����� .R��� $ 
Horizontal=�.+��� $, 	Separator��$- .5��$. 3��$/ .5��$0 12��$��7,0,5,0q�#���  �� $!��NavigationCommands.GoToPage��8�� ���� *DisplayName+���� *Source+���� $1 ContentFrame��2 �� 3 ��   �� *PreviousData+4 Z� .a��� $5 	Separator�� � y�$ 	Collapsed=�  $��1��$��36,0,0,0q�#7 z6 8 �� *Source+$1 ContentFrame��$9 TwoWay=�  a: ��@����O�@������p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O�@����O$#��NavigationCommands.BrowseBack��$
 back��$��2��$
��Left=�$	��Top=�$��	34,24,0,0q� . ���� $1 ContentFrame��  $; ContentFrame��$��3��$��2��$��
36,8,16,16q�#= z< > �� *ContentSource+  �� *TemplatedParent+$9 TwoWay=�� $��4��.+�F� $? 
ResizeGrip��$y�	Collapsed=�$��12��$��12��$��1q�$��Right=�#���  $��1��$
 None=������@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- �� #���  $��1q�y��� *
IsActive+  �� *FindAncestor+"" �8  @ P� A �@�WindowState$B 	Maximized=�. ��� $5 Maximize�� � y�$ 	Collapsed=��� $5 Restore�� � y�$ Visible=��� $5 
LayoutRoot�� � ��$ 7q�P� A �@�WindowState$B Normal=�. ��� $5 Maximize�� � y�$ Visible=��� $5 Restore�� � y�$ 	Collapsed=�]� C �� D �@�
ResizeMode$E CanResizeWithGrip=��� D �@�WindowState$E Normal=�./��� $5 
ResizeGrip�� � y�$ Visible=���  �@ WindowChrome   $F 0��$G 1q�H .  $
I None=�