   M S B A M L   `   `   `  ����V  RMemberSignInSystem.ModernUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MemberSignInSystem.ModernUI.ViewModels&MemberSignInSystem.ModernUI.ViewModels  5      Y1clr-namespace:MemberSignInSystem.ModernUI.Content#MemberSignInSystem.ModernUI.Content  5      Y1clr-namespace:MemberSignInSystem.ModernUI.Content#MemberSignInSystem.ModernUI.Content  5	      7    1MemberSignInSystem.ModernUI.Content.MemberDisplay   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dvms4clr-namespace:MemberSignInSystem.ModernUI.ViewModels     5      N JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5      ?content1clr-namespace:MemberSignInSystem.ModernUI.Content  5      <this1clr-namespace:MemberSignInSystem.ModernUI.Content  5	        � 	Resources  5         IndividualDefaultImage �� 	UriSource  IndividualTemplate �� Orientation �� TargetNullValue  Heading2�� 5   
   %�  &	        &	 T     7   7  7  7  7  �� 5      $@ :/MemberSignInSystem.ModernUI;component/Images/Original.jpg?�69   �� 5      .R�5      �� $��7q�6   ��6$   �� *DisplayInformation+��6K   �� ��5      �� 5      $ Vertical=�6%   
������ $��5q�6U   .+�5      �� $��Uniform=�6$   $	��160��66   $	��160��6C   ��6O   �� *PicturePath+8   �� 5      8�� $��Center=�6^   ��6(   �� *Id+6^   �� 5      $ 
Horizontal=�6)   $��Center=�6B   .+�5   "   �� 8�� ��6,   �� *	FirstName+6G   �� 5   "   $�� ��6,   8�� 65   �� 5   "   8�� ��6,   �� *
LastName+6F   5      5      5      5      5      .��5!      � .+�5"   
   7   1MemberSignInSystem.ModernUI.Content.FlipImageGrid  -   5          Name$ 	ImageGrid��5"         PicturesDirectory   ImageDirectory# �  63     OneImageBool   OneImageBool# �  6h   � 5#   
   y�6   �� *MemberDisplayIsVisible+.+�5$      �  � DataContext 6   �� *DisplayedMember+.+�5%      �� $��Stretch=�6   $��Stretch=�6:    J� Opacity$	 0.7Q�6q   ��6V   �� *	ColorCode+6q   �� 5&      $ Vertical=�6   $
��Auto��64   .+�5'      � 	 � Orientation$	 
Horizontal=�6    $��Center=�69   .+�5(      �� 
�������  Heading1#��[ 6K   $
��Bold"�6m   ��6$   �� *Id+6m   �� 5)      $��: The ��6$   
�������#��[ 6E   $
��Bold"�6g   �� 5*      
�������#��[ 6S   $
��Bold"�6u   ��6$   �� *
FamilyName+6u   �� 5+      $�� family��6$   
�������#��[ 6F   $
��Bold"�6h   5,      � 56      
 � ColumnDefinitions
 57      �� 58      $��* �6/   �� 59      �� 5:      $��* �6/   .+�5<      �� $��7q�6!   $��1��6,   ��6<   �� *DisplayInformation+��6c   �� ��5=      �� 5>   "   $��Uniform=�6(   ��6:   �� *PicturePath+# [  5@      5A      5B      5C      5D      5E      5F      