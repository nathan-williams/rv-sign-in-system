   M S B A M L   `   `   `  ���� F  BOpenWeather, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:OpenWeather.ViewModelOpenWeather.ViewModel  ;"clr-namespace:OpenWeather.BehaviorOpenWeather.Behavior      OpenWeather.WeatherApplet   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2vm#clr-namespace:OpenWeather.ViewModel     2bvr"clr-namespace:OpenWeather.Behavior     AControls,http://metro.mahapps.com/winfx/xaml/controls     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  N JFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b159elysiumcontrols#http://schemas.codeplex.com/elysium  -     >�Name$  WeatherControl�� � 	Resources    ForecastDataTemplate �� Padding �� TextAlignment �� Orientation�� %%  &	        �� .R�� $��0,2,0,0q�$��83��.+��� ���	#FF5E727C$��0,1,0,0q�$��Center=�$��83��$��0q�$��Center=�$	��265��._��� $��0q�.+�� $��24��$��0,5,0,0q�.+��� $
��Wrap=�$��14(����	#FF333B42$ 2,0,0,0q�$
��72.5��$��0q�$ Center=�$
��Bold"�$
��Left=����� *
WeatherDay+�� $
��Wrap=�$��14(����	#FF292929$ 0q�$��
77.5,0,0,0q�$
 Left=�$��Normal"�$��Segoe UI Semibold)����� *	Weather+� $
��51.5��.+��� $��0q�$ 
Horizontal=�$
��Left=�.+��� $
��Left=�$
��71.5������ *WeatherIcon+�� $
��Left=�$
��51.5��$
��Wrap=�$	��Top=�$
��83.5��$��Segoe UI)�$��24(�$ Center=�$ 0,5,0,0q����	#FFA47D14���� *Temperature+�� $��
0,26,0,3.5q�$ 
Horizontal=�$��Right=�$	��115��.+��� $
��Wrap=�$��Wind:��$��Segoe UI Semibold)�$��14(�$ Center=�$
��49.5���� $
��Wrap=�$��Segoe UI Semibold)�$��14(����	#FF9B8C5E$ 0q�$��76.666������ *	WindSpeed+�� $��0,4,0,25q�$ 
Horizontal=�$��Right=�$	��120��.+��� $��22��$
��Wrap=�$	��Top=�$��65��$��Segoe UI Semibold)�$��14(����	#FF056C9C$
��Left=�$ 0q�$ Center=����� *MaxTemperature+�� $
��Wrap=�$��Segoe UI Semibold)�$��14(����#0033EE$��0q�$ 0q�$��67������ *MinTemperature+ � DataContext ,  &OpenWeather.ViewModel.WeatherViewModel  .��� -   $��WeatherAppletContent��.+�� -   $��WeatherDisplay��$y�Hidden=�.+��� $ Vertical=�$��Center=�.+��� $ 
Horizontal=�$��Center=�.+��� $
��Wrap=�$��14(�$��5,0,0,0q�$
 Left=�$
��Bold"�$��Center=����� *!CurrentWeather.TimeOfLastUpdate+�� $ 
Horizontal=�$��Center=�$	��Top=�.+��� -    ��Name$ CurrentWeatherImage��$��95��$	��Top=�$	��137��$��4,0,0,0q����� *CurrentWeather.WeatherIcon+�� -    ��Name$ CurrentWeatherTemperature��$��Right=�$��95��$
��Wrap=�$	��Top=�$	��143��$��45.333(�$ Center=�$ 5,15,0,0q�$��Segoe UI)����� *CurrentWeather.Temperature+�� -   $ CurrentWeatherTextBlock��$��Center=�$��25��$��7.5,114,7.5,0q�$
��Wrap=�$	��Top=�$	��274��$��14.667(�$��Segoe UI Semibold)�$ 0q�$ Center=����� *CurrentWeather.Weather+�� -    ��Name$ CurrentWeatherStackPanel��$��16,142,14,0q�$ 
Horizontal=�$	��Top=�.+��� -   $ MaxTempTextBlock��$��22��$
��Wrap=�$	��Top=�$��65��$��Segoe UI Semibold)�$��14.667(����#DB0000$
��Left=�$ 0q�$ Center=����� *CurrentWeather.MaxTemperature+�� $
��Left=�$��22��$	��Top=�$��67��$	��1,0q����	#FF5E727C._��� -	   $ MinTempTextBlock��$
��Wrap=�$��Segoe UI Semibold)�$��14.667(����#0033EE$��0,0,-1,0q�$ 0q�$ Center=����� *CurrentWeather.MinTemperature+�� $
��Wrap=�$��Wind:��$��Segoe UI Semibold)�$��14.667(�$
��54.5��$ Right=��� $
��Wrap=�$��Segoe UI Semibold)�$��14.667(����	#FF9B8C5E$��79.999��$ 7,0,0,0q����� *CurrentWeather.WindSpeed+7  1FirstFloor.ModernUI.Windows.Controls.ModernButton  -
   $��RefreshWeatherButton��	   IconData�	 ����C�:�����:����Z�0n�����	��M�<R�|�M���0P���р�C��р�����������yȢ���CȢ����Ȣ�4���`;���+��(;��+��]G�8�p4��:��C�:���C8�����8�̟�0��<o�D�[�$�LD�[�$M@r��v�|�M��C|�M�0��|�M��|�-4���<��4���}ф:Tn5ф:l't��Z�l'tр�C���Ƽ���������8+8���C8�
 � ToolTip$
 refresh��{�.  $��Right=�$	��Top=�'  !System.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome . ���� *WeatherCommand+�� $��Right=�$	��Top=�.@�#  Elysium.Controls.ProgressRing  $��18��$��18��$��0,1,24,0q� 
 Accent#���  .  (Elysium.Controls.Primitives.ProgressBase   State$ Indeterminate=� �� *	IsLoading+y���    BooleanToVisibilityConverter �� 	Converter# [ �� -   $��InitialProgressRing��$��Center=�$��Center=�.@�  $��50��$��50��#���  $��20(�$ Indeterminate=�$y�Visible=�