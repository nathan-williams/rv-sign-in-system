   M S B A M L   `   `   `  ���� N  JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:FirstFloor.ModernUI.Windows.Controls$FirstFloor.ModernUI.Windows.Controls  9!clr-namespace:FirstFloor.ModernUIFirstFloor.ModernUI  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b15  �� MergedDictionaries �� Source   AdvancedIcon @� Center @� RadiusX @� RadiusY  MinimizeIcon � FillRule �� Rect  MaximizeIcon �� 
StartPoint �� EndPoint  RestoreIcon  	CloseIcon  CustomWindow	 �� 
TargetType7    1FirstFloor.ModernUI.Windows.Controls.ModernWindow
 �� BasedOn �� Property  WindowBackgroundContent �� Value  DefaultFontFamily  DefaultFontSize 	 
WindowText&   System.Windows.Media.TextOptions �� 
TargetType ��Name%  System.Windows.SystemParameters$   WindowNonClientFrameThickness �� Source 
 WindowBackgroundColor �� Color � 	Resources  Key �� 
TargetName �� TargetProperty	 �� To g� Duration  WindowBorder �Name � RowDefinitions    BackgroundContent 
 Accent  ModernWindowTitle    IsTitleVisible � DataContext  ToUpperConverter �� 	Converter   BooleanToVisibilityConverter �� 	Converter � LayoutTransform �� Angle     LogoData  NullToVisibilityConverter! �� ConverterParameter" �� Stretch# �� Orientation'  !System.Windows.Shell.WindowChrome$   IsHitTestVisibleInChrome#   FirstFloor.ModernUI.Resources%   Minimize& � ToolTip  SystemButton#  System.Windows.SystemCommands'   MinimizeWindowCommand( �� RelativeSource) �� Mode* �� AncestorType+ ��Name,   Restore-   RestoreWindowCommand. � UseLayoutRounding/   Maximize0   MaximizeWindowCommand1   Close2   CloseWindowCommand3    
TitleLinks4 ��Name	5 �� X1	6 �� Y1	7 �� X2	8 �� Y2  SeparatorBackground  SystemButtonLink9 �� ElementName: �� Triggers; �� Binding< �� Value= �� 
TargetName#  Elysium.Controls.ProgressRing.  (Elysium.Controls.Primitives.ProgressBase>   State?    IsProgressRingVisible5   /FirstFloor.ModernUI.Windows.Controls.ModernMenu@    MenuLinkGroupsA   
LinkGroupsB   SelectedSourceC �� Mode7	   1FirstFloor.ModernUI.Windows.Controls.ModernButtonD 	  IconData6
   0FirstFloor.ModernUI.Windows.Controls.ModernFrameE 
 NameF    ContentLoaderG 
  ContentLoaderH 
  SourceI F�Name  WindowBorderActiveJ �� TriggersK P� PropertyL P� ValueM ]� 
ConditionsN �� PropertyO �� ValueP   CornerRadiusQ   GlassFrameThicknessR   UseAeroCaptionButtonsS   NonClientFrameEdges  EmptyWindow�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ?controls2clr-namespace:FirstFloor.ModernUI.Windows.Controls  .modernui!clr-namespace:FirstFloor.ModernUI  9elysiumcontrols#http://schemas.codeplex.com/elysium  >elysiumparams*http://schemas.codeplex.com/elysium/params    �� $7 1/FirstFloor.ModernUI;component/Assets/Button.xaml?��� $; 5/FirstFloor.ModernUI;component/Assets/Converters.xaml?�%=  &	        &	 s     &	 �     &	 m    &	 �    &	 j    7  7  7  7  7  7  7  7  7  7  7  &	 p    7  7  7  7  7  7  � .��@� $	 3,3&�$	 2.5Q�$	 2.5Q�@� $
 12,3&�$	 2.5Q�$	 2.5Q�@� $
 21,3&�$	 2.5Q�$	 2.5Q�� $ EvenOdd=�.���� $ 	0,0,10,10��� $ 0,0,10,7�� .���� $ 0,0.5&�$ 10,0.5&��� $ 0,1.5&�$ 10,1.5&��� $ 0,2.5&�$ 10,2.5&��� $ 0.5,3&�$ 0.5,10&��� $ 9.5,3&�$ 9.5,10&��� $ 1,9.5&�$ 9,9.5&�� .���� $ 3,0.5&�$ 10,0.5&��� $ 3,1.5&�$ 10,1.5&��� $ 3.5,2&�$ 3.5,3&��� $ 9.5,2&�$ 9.5,7&��� $ 7,6.5&�$ 10,6.5&��� $ 0,3.5&�$ 7,3.5&��� $ 0,4.5&�$ 7,4.5&��� $ 0.5,5&�$ 0.5,10&��� $ 6.5,5&�$ 6.5,10&��� $ 1,9.5&�$ 6,9.5&�� .���� $ 1.5,0.5&�$ 9.5,8.5&��� $ 0.5,1.5&�$ 8.5,9.5&��� $ 1.5,1.5&�$ 8.5,8.5&��� $ 8.5,0.5&�$ 0.5,8.5&��� $ 9.5,1.5&�$ 1.5,9.5&��� $ 8.5,1.5&�$ 1.5,8.5&��� "	   8
   .���  �@  BackgroundContent# �  ��  � ��# �  ��  � ��# �  ��  � ��# � 	 ��  � ��$	 800����  � ��$	 640����  � ��$	 320����  � ��$	 320����  �@�
ResizeMode$ CanResizeWithGrip=���  �@�UseLayoutRounding . ��  �@ TextFormattingMode$ Display=���  � �� �� "   .R��� $ WindowBorder������ # Z ���� WindowBorderBackground��# � 
  ���� BackgroundAnimation .���� $ WindowBorderBackground��$ Color�# � 
 $ 0:0:.6D�._�� .+��� #���  $��1q�._��� .u�� $ 
LayoutRoot�� �� $��36 ��� $
��Auto ��� $
��Auto ��� $��* ��� $��15 �.+��� $��5��#��z �� $��5��#���  $z�1,0,0,1,0,32W�$��14��$��53��$
��Left=�$	��Top=�._��� 
�������$	��0,8q�#���  $��Center=�# z ���� *Title+8  y��� 8   �� $	 -90Q��� #���  $��36��$��36��$��Right=�$	��Top=�$��0,0,76,0q�# z  y��� 8  $! inverse��._�F� $
" Fill=�
�������$��24��$��24��$��Center=�$��Center=����� �� $# 
Horizontal=�$��Right=�$	��Top=�$��0,8,6,0q�$ . .+��� #& Z% 8�� ���� # Z' ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� ����    ��[ � z ��[ 	$��8��$��7��$��Center=�$��Center=�$��2������ *
Foreground+( �� $) FindAncestor=�$* ButtonL�� $��1,0,1,0q�.+��� $+ Restore��#& Z, 8�� $y�	Collapsed=����� # Z- ��� $��13��$��12��. . $z�1,0,0,1,.5,.5W�.+�F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��$��8��$��Center=�$��Center=�$��1������ *
Foreground+( �� $) FindAncestor=�$* ButtonL��� $+ Maximize��#& Z/ 8�� ���� # Z0 ��� $��13��$��12��.+�F� +����    @B �@T� @B �@T�  z �     z 	$��9��$��8��$��Center=�$��Center=�$��2������ *
Foreground+( �� $) FindAncestor=�$* ButtonL��� #& Z1 8�� ���� # Z2 ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� +����        � z ��j � z     �    ��j 	$��8��$��7��$��Center=�$��Center=�$	��1.5������ *
Foreground+( �� $) FindAncestor=�$* ButtonL��� #��z3 $��Right=�$	��Top=�$��	0,6,180,0q�$ . ���� .R��� $# 
Horizontal=����� .R��� $# 
Horizontal=�.+��� $4 	Separator��$5 .5��$6 3��$7 .5��$8 12��$��7,0,5,0q�#���  �� $!��NavigationCommands.GoToPage��8�� ���� *DisplayName+���� *Source+���� $9 ContentFrame��: �� ; �� ( �� *PreviousData+< Z� .a��� $= 	Separator�� � y�$ 	Collapsed=��� $��Right=�$	��Top=�.@�  $��25��$��25��$��	0,6,135,0q�#���  $> Indeterminate=�# z? y��� 8 	   $��1��$��36,0,0,0q�#A z@ B �� *Source+$9 ContentFrame��$C TwoWay=�	  �D ���Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś$!��NavigationCommands.GoToPage��$D��>/MemberSignInSystem.ModernUI;component/Content/Pages/Home.xaml��$
& home��$��2��$
��Left=�$	��Top=�$��	34,24,0,0q�$ . ���� $9 ContentFrame��
  $E ContentFrame��$��3��$��2��$��
36,8,16,16q�#G zF H �� *ContentSource+( �� *TemplatedParent+$C TwoWay=�� $��4��.+�F� $I 
ResizeGrip��$y�	Collapsed=�$��12��$��12��$��1q�$��Right=�#��� 	 $��1��$
" None=������@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- �� #���  $��1q�y��� *
IsActive+( �� *FindAncestor+"* �8 
 J P� K �@�WindowState$L 	Maximized=�. ��� $= Maximize�� � y�$ 	Collapsed=��� $= Restore�� � y�$ Visible=��� $= 
LayoutRoot�� � ��$ 7q�P� K �@�WindowState$L Normal=�. ��� $= Maximize�� � y�$ Visible=��� $= Restore�� � y�$ 	Collapsed=�]� M �� N �@�
ResizeMode$O CanResizeWithGrip=��� N �@�WindowState$O Normal=�./��� $= 
ResizeGrip�� � y�$ Visible=���  �@ WindowChrome   $P 0��$Q 1q�R .  $
S None=��� "	   8
   .���  � �� �� "   .R��� $ WindowBorder������ # Z ���� WindowBorderBackground��# � 
  ���� BackgroundAnimation .���� $ WindowBorderBackground��$ Color�# � 
 $ 0:0:.6D�._�� .+��� #���  $��1q�._��� .u�� $ 
LayoutRoot�� �� $��* ��� $��15 �.+��� $��2��#��z �� $��2��#��z���� $# 
Horizontal=�$��Right=�$	��Top=�$��0,8,6,0q�$ . .+��� #& Z% 8�� ���� # Z' ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� ����    ��[ � z ��[ 	$��8��$��7��$��Center=�$��Center=�$��2������ *
Foreground+( �� $) FindAncestor=�$* ButtonL�� $��1,0,1,0q�.+��� $+ Restore��#& Z, 8�� $y�	Collapsed=����� # Z- ��� $��13��$��12��. . $z�1,0,0,1,.5,.5W�.+�F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��$��8��$��Center=�$��Center=�$��1������ *
Foreground+( �� $) FindAncestor=�$* ButtonL��� $+ Maximize��#& Z/ 8�� ���� # Z0 ��� $��13��$��12��.+�F� +����    @B �@T� @B �@T�  z �     z 	$��9��$��8��$��Center=�$��Center=�$��2������ *
Foreground+( �� $) FindAncestor=�$* ButtonL��� #& Z1 8�� ���� # Z2 ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� +����        � z ��j � z     �    ��j 	$��8��$��7��$��Center=�$��Center=�$	��1.5������ *
Foreground+( �� $) FindAncestor=�$* ButtonL�� $��1��.+�F� $I 
ResizeGrip��$y�	Collapsed=�$��12��$��12��$��1q�$��Right=�#��� 	 $��1��$
" None=������@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- �� #���  $��1q�y��� *
IsActive+( �� *FindAncestor+"* �8  J P� K �@�WindowState$L 	Maximized=�. ��� $= Maximize�� � y�$ 	Collapsed=��� $= Restore�� � y�$ Visible=��� $= 
LayoutRoot�� � ��$ 7q�P� K �@�WindowState$L Normal=�. ��� $= Maximize�� � y�$ Visible=��� $= Restore�� � y�$ 	Collapsed=�]� M �� N �@�
ResizeMode$O CanResizeWithGrip=��� N �@�WindowState$O Normal=�./��� $= 
ResizeGrip�� � y�$ Visible=�