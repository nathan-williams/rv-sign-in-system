   M S B A M L   `   `   `  ����F  BOpenWeather, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:OpenWeather.ViewModelOpenWeather.ViewModel  5      ;"clr-namespace:OpenWeather.BehaviorOpenWeather.Behavior  5          OpenWeather.WeatherApplet   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2vm#clr-namespace:OpenWeather.ViewModel     5      2bvr"clr-namespace:OpenWeather.Behavior     5      AControls,http://metro.mahapps.com/winfx/xaml/controls     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      N JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5	      Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b159elysiumcontrols#http://schemas.codeplex.com/elysium  5
      -   5          >�Name$  WeatherControl��5       � 	Resources 5         ForecastDataTemplate �� Padding �� TextAlignment �� Orientation�� 5   
   %�  &	        �� 5      .R�5      � $��0,2,0,0q�6   $��83��6(   .+�5      �� ���	#FF5E727C$��0,1,0,0q�65   $��Center=�6O   $��83��6l   $��0q�5      $��Center=�6   $	��265��6-   ._�5      �� $��0q�6%   .+�5      � $��24��6#   $��0,5,0,0q�6/   .+�5   "   �� $
��Wrap=�6,   $��14(�6@   ���	#FF333B42$ 2,0,0,0q�6e   $
��72.5��6w   $��0q�5   %   $ Center=�60   $
��Bold"�6G   $
��Left=�6Z   ��5   %   �� *
WeatherDay+�� 5   "   $
��Wrap=�6,   $��14(�6@   ���	#FF292929$ 0q�6e   $��
77.5,0,0,0q�6q   $
 Left=�5   %   $��Normal"�6:   $��Segoe UI Semibold)�6O   ��5   %   �� *	Weather+5      � 5      $
��51.5��6#   .+�5   "   �� $��0q�6-   $ 
Horizontal=�68   $
��Left=�6Q   .+�5    &   �� $
��Left=�6,   $
��71.5��6G   ��6T   �� *WeatherIcon+�� 5!   &   $
��Left=�60   $
��51.5��6K   $
��Wrap=�6Y   $	��Top=�6m   $
��83.5��5"   )   $��Segoe UI)�66   $��24(�6L   $ Center=�6Z   $ 0,5,0,0q�5#   )   ���	#FFA47D14��6S   �� *Temperature+5$   #   �� 5%   "   $��
0,26,0,3.5q�6-   $ 
Horizontal=�6A   $��Right=�6Z   $	��115��6v   .+�5&   &   �� $
��Wrap=�60   $��Wind:��6D   $��Segoe UI Semibold)�6Q   $��14(�6p   $ Center=�5'   )   $
��49.5��6@   �� 5(   &   $
��Wrap=�60   $��Segoe UI Semibold)�6D   $��14(�6c   ���	#FF9B8C5E$ 0q�5)   @   $��76.666��6L   ��6[   �� *	WindSpeed+5*   #   �� 5+   "   $��0,4,0,25q�6-   $ 
Horizontal=�6?   $��Right=�6X   $	��120��6t   .+�5,   &   �� $��22��60   $
��Wrap=�6<   $	��Top=�6P   $��65��6h   $��Segoe UI Semibold)�5-   )   $��14(�6H   ���	#FF056C9C$
��Left=�5.   )   $ 0q�6D   $ Center=�6P   ��5/   )   �� *MaxTemperature+�� 50   &   $
��Wrap=�60   $��Segoe UI Semibold)�6D   $��14(�6c   ���#0033EE$��0q�51   >   $ 0q�6I   $��67��6U   ��52   )   �� *MinTemperature+53   #   54      55      56      57      58      59       � DataContext 5<      ,  &OpenWeather.ViewModel.WeatherViewModel  5=   
   .��5@      � -   5        $��WeatherAppletContent��5@      .+�5A   
   � -   5        $��WeatherDisplay��5A      $y�Hidden=�6%   .+�5B      �� $ Vertical=�6   $��Center=�60   .+�5C      �� $ 
Horizontal=�6   $��Center=�66   .+�5I      �� $
��Wrap=�6    $��14(�64   $��5,0,0,0q�5J      $
 Left=�6*   $
��Bold"�6?   $��Center=�6R   ��5K      �� *!CurrentWeather.TimeOfLastUpdate+5L      �� 5M      $ 
Horizontal=�6   $��Center=�66   $	��Top=�5N      .+�5P      �� -   5         ��Name$ CurrentWeatherImage��5P      $��95��69   $	��Top=�6E   $	��137��6]   $��4,0,0,0q�6i   ��5Q      �� *CurrentWeather.WeatherIcon+�� 5R      -   5         ��Name$ CurrentWeatherTemperature��5R       $��Right=�6C   $��95��6_   $
��Wrap=�5S      $	��Top=�6-   $	��143��6E   $��45.333(�6Q   $ Center=�5T      $ 5,15,0,0q�60   $��Segoe UI)�6C   ��5U      �� *CurrentWeather.Temperature+5W      5X      �� 5e      -   5        $ CurrentWeatherTextBlock��5e      $��Center=�69   $��25��6V   $��7.5,114,7.5,0q�5f      $
��Wrap=�6$   $	��Top=�68   $	��274��6P   $��14.667(�5g      $��Segoe UI Semibold)�6   $ 0q�6>   $ Center=�5h      ��6$   �� *CurrentWeather.Weather+�� 5j      -   5         ��Name$ CurrentWeatherStackPanel��5j      $��16,142,14,0q�6;   $ 
Horizontal=�6P   $	��Top=�6i   .+�5k      �� -   5        $ MaxTempTextBlock��5k      $��22��66   $
��Wrap=�6B   $	��Top=�6V   $��65��5l      $��Segoe UI Semibold)�6   $��14.667(�6;   ���#DB0000$
��Left=�5m      $ 0q�6,   $ Center=�68   ��5n      �� *CurrentWeather.MaxTemperature+�� 5o      $
��Left=�6   $��22��64   $	��Top=�6@   $��67��6X   $	��1,0q�5p      ���	#FF5E727C._�5q      �� -	   5        $ MinTempTextBlock��5q       $
��Wrap=�6:   $��Segoe UI Semibold)�6N   $��14.667(�5r      ���#0033EE$��0,0,-1,0q�6<   $ 0q�6N   $ Center=�6Z   ��5s      �� *CurrentWeather.MinTemperature+5t      �� 5u      $
��Wrap=�6   $��Wind:��60   $��Segoe UI Semibold)�6=   $��14.667(�6\   $
��54.5��5v      $ Right=�6   �� 5x      $
��Wrap=�6   $��Segoe UI Semibold)�60   $��14.667(�6O   ���	#FF9B8C5E$��79.999��5y   (   $ 7,0,0,0q�67   ��5z      �� *CurrentWeather.WindSpeed+5{      5�      7  1FirstFloor.ModernUI.Windows.Controls.ModernButton  5�   
   -
   5        $��RefreshWeatherButton��5�   ?   	   IconData�	 ����C�:�����:����Z�0n�����	��M�<R�|�M���0P���р�C��р�����������yȢ���CȢ����Ȣ�4���`;���+��(;��+��]G�8�p4��:��C�:���C8�����8�̟�0��<o�D�[�$�LD�[�$M@r��v�|�M��C|�M�0��|�M��|�-4���<��4���}ф:Tn5ф:l't��Z�l'tр�C���Ƽ���������8+8���C8�
 � ToolTip$
 refresh��6   {�.  $��Right=�5�      $	��Top=�67   '  !System.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome . ��5�      �� *WeatherCommand+5�      �� 5�   
   $��Right=�6   $	��Top=�63   .@�5�      #  Elysium.Controls.ProgressRing  $��18��6+   $��18��67   $��0,1,24,0q�6B    
 Accent#���  5�   +   .  (Elysium.Controls.Primitives.ProgressBase   State$ Indeterminate=�5�   +    5�   +   �� *	IsLoading+y�5�   +   ��    BooleanToVisibilityConverter �� 	Converter# [ 5�      �� 5�   
   -   5        $��InitialProgressRing��5�      $��Center=�62   $��Center=�6O   .@�5�        $��50��6+   $��50��67   #���  5�   +   $��20(�5�   +   $ Indeterminate=�5�   +   $y�Visible=�5�   +   5�      5�      5�      