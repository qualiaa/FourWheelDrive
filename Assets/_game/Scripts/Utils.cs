using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplashJam {
	
	public static class Utils {
		
		public static string GetSecondsAndMinutesText( float time, float decialTextSize )
		{
			float timeDecimals = ( time - Mathf.Floor( time ) ) * 100f;

			string secondsText = string.Format(
				"{0}<size={1}>.{2}</size>",
				time.ToString( "0" ),
				decialTextSize,
				timeDecimals.ToString( "00" ).Substring( 0, 2 ) );
			return secondsText;
		}
	}
}