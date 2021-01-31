// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ToonShader"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_Intensity("Intensity", Float) = 1
		_Postcount("Post count", Float) = 1
		_Step("Step", Float) = 8
		_Color0("Color 0", Color) = (0,0,0,0)

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			

			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float _Step;
			uniform float _Intensity;
			uniform float4 _Color0;
			uniform float _Postcount;
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv0_MainTex = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 localCenter138_g194 = uv0_MainTex;
				float4 break13 = ( _MainTex_TexelSize * _Step );
				float temp_output_2_0_g194 = break13.x;
				float localNegStepX156_g194 = -temp_output_2_0_g194;
				float temp_output_3_0_g194 = break13.y;
				float localStepY164_g194 = temp_output_3_0_g194;
				float2 appendResult14_g202 = (float2(localNegStepX156_g194 , localStepY164_g194));
				float4 tex2DNode16_g202 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g202 ) );
				float temp_output_2_0_g202 = (tex2DNode16_g202).r;
				float temp_output_4_0_g202 = (tex2DNode16_g202).g;
				float temp_output_5_0_g202 = (tex2DNode16_g202).b;
				float localTopLeft172_g194 = ( sqrt( ( ( ( temp_output_2_0_g202 * temp_output_2_0_g202 ) + ( temp_output_4_0_g202 * temp_output_4_0_g202 ) ) + ( temp_output_5_0_g202 * temp_output_5_0_g202 ) ) ) * _Intensity );
				float2 appendResult14_g198 = (float2(localNegStepX156_g194 , 0.0));
				float4 tex2DNode16_g198 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g198 ) );
				float temp_output_2_0_g198 = (tex2DNode16_g198).r;
				float temp_output_4_0_g198 = (tex2DNode16_g198).g;
				float temp_output_5_0_g198 = (tex2DNode16_g198).b;
				float localLeft173_g194 = ( sqrt( ( ( ( temp_output_2_0_g198 * temp_output_2_0_g198 ) + ( temp_output_4_0_g198 * temp_output_4_0_g198 ) ) + ( temp_output_5_0_g198 * temp_output_5_0_g198 ) ) ) * _Intensity );
				float localNegStepY165_g194 = -temp_output_3_0_g194;
				float2 appendResult14_g201 = (float2(localNegStepX156_g194 , localNegStepY165_g194));
				float4 tex2DNode16_g201 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g201 ) );
				float temp_output_2_0_g201 = (tex2DNode16_g201).r;
				float temp_output_4_0_g201 = (tex2DNode16_g201).g;
				float temp_output_5_0_g201 = (tex2DNode16_g201).b;
				float localBottomLeft174_g194 = ( sqrt( ( ( ( temp_output_2_0_g201 * temp_output_2_0_g201 ) + ( temp_output_4_0_g201 * temp_output_4_0_g201 ) ) + ( temp_output_5_0_g201 * temp_output_5_0_g201 ) ) ) * _Intensity );
				float localStepX160_g194 = temp_output_2_0_g194;
				float2 appendResult14_g195 = (float2(localStepX160_g194 , localStepY164_g194));
				float4 tex2DNode16_g195 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g195 ) );
				float temp_output_2_0_g195 = (tex2DNode16_g195).r;
				float temp_output_4_0_g195 = (tex2DNode16_g195).g;
				float temp_output_5_0_g195 = (tex2DNode16_g195).b;
				float localTopRight177_g194 = ( sqrt( ( ( ( temp_output_2_0_g195 * temp_output_2_0_g195 ) + ( temp_output_4_0_g195 * temp_output_4_0_g195 ) ) + ( temp_output_5_0_g195 * temp_output_5_0_g195 ) ) ) * _Intensity );
				float2 appendResult14_g196 = (float2(localStepX160_g194 , 0.0));
				float4 tex2DNode16_g196 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g196 ) );
				float temp_output_2_0_g196 = (tex2DNode16_g196).r;
				float temp_output_4_0_g196 = (tex2DNode16_g196).g;
				float temp_output_5_0_g196 = (tex2DNode16_g196).b;
				float localRight178_g194 = ( sqrt( ( ( ( temp_output_2_0_g196 * temp_output_2_0_g196 ) + ( temp_output_4_0_g196 * temp_output_4_0_g196 ) ) + ( temp_output_5_0_g196 * temp_output_5_0_g196 ) ) ) * _Intensity );
				float2 appendResult14_g197 = (float2(localStepX160_g194 , localNegStepY165_g194));
				float4 tex2DNode16_g197 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g197 ) );
				float temp_output_2_0_g197 = (tex2DNode16_g197).r;
				float temp_output_4_0_g197 = (tex2DNode16_g197).g;
				float temp_output_5_0_g197 = (tex2DNode16_g197).b;
				float localBottomRight179_g194 = ( sqrt( ( ( ( temp_output_2_0_g197 * temp_output_2_0_g197 ) + ( temp_output_4_0_g197 * temp_output_4_0_g197 ) ) + ( temp_output_5_0_g197 * temp_output_5_0_g197 ) ) ) * _Intensity );
				float temp_output_133_0_g194 = ( ( localTopLeft172_g194 + ( localLeft173_g194 * 2 ) + localBottomLeft174_g194 + -localTopRight177_g194 + ( localRight178_g194 * -2 ) + -localBottomRight179_g194 ) / 6.0 );
				float2 appendResult14_g200 = (float2(0.0 , localStepY164_g194));
				float4 tex2DNode16_g200 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g200 ) );
				float temp_output_2_0_g200 = (tex2DNode16_g200).r;
				float temp_output_4_0_g200 = (tex2DNode16_g200).g;
				float temp_output_5_0_g200 = (tex2DNode16_g200).b;
				float localTop175_g194 = ( sqrt( ( ( ( temp_output_2_0_g200 * temp_output_2_0_g200 ) + ( temp_output_4_0_g200 * temp_output_4_0_g200 ) ) + ( temp_output_5_0_g200 * temp_output_5_0_g200 ) ) ) * _Intensity );
				float2 appendResult14_g199 = (float2(0.0 , localNegStepY165_g194));
				float4 tex2DNode16_g199 = tex2D( _MainTex, ( localCenter138_g194 + appendResult14_g199 ) );
				float temp_output_2_0_g199 = (tex2DNode16_g199).r;
				float temp_output_4_0_g199 = (tex2DNode16_g199).g;
				float temp_output_5_0_g199 = (tex2DNode16_g199).b;
				float localBottom176_g194 = ( sqrt( ( ( ( temp_output_2_0_g199 * temp_output_2_0_g199 ) + ( temp_output_4_0_g199 * temp_output_4_0_g199 ) ) + ( temp_output_5_0_g199 * temp_output_5_0_g199 ) ) ) * _Intensity );
				float temp_output_135_0_g194 = ( ( -localTopLeft172_g194 + ( localTop175_g194 * -2 ) + -localTopRight177_g194 + localBottomLeft174_g194 + ( localBottom176_g194 * 2 ) + localBottomRight179_g194 ) / 6.0 );
				float temp_output_111_0_g194 = sqrt( ( ( temp_output_133_0_g194 * temp_output_133_0_g194 ) + ( temp_output_135_0_g194 * temp_output_135_0_g194 ) ) );
				float3 appendResult113_g194 = (float3(temp_output_111_0_g194 , temp_output_111_0_g194 , temp_output_111_0_g194));
				float4 temp_output_19_0 = ( float4( appendResult113_g194 , 0.0 ) * _Color0 );
				float3 hsvTorgb48 = RGBToHSV( ( temp_output_19_0 + tex2D( _MainTex, uv0_MainTex ) ).rgb );
				float temp_output_54_0 = ( hsvTorgb48.z * _Postcount );
				float3 hsvTorgb56 = HSVToRGB( float3(hsvTorgb48.x,hsvTorgb48.y,( round( temp_output_54_0 ) / _Postcount )) );
				

				finalColor = ( temp_output_19_0 + float4( hsvTorgb56 , 0.0 ) );

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18000
0;73;1623;706;-361.9189;-200.7354;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;18;-616.7659,-61.46041;Float;False;Property;_Step;Step;21;0;Create;True;0;0;False;0;8;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;12;-668.6111,-270.4125;Inherit;False;0;0;_MainTex_TexelSize;Pass;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-385.2833,-136.6781;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-700.4827,137.4609;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;13;-181.2512,-208.7156;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-199.8508,145.4844;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;39;140.4489,-118.4155;Inherit;False;SobelMain;0;;194;481788033fe47cd4893d0d4673016cbc;0;4;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT2;0,0;False;1;SAMPLER2D;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;21;129.2993,57.32825;Float;False;Property;_Color0;Color 0;22;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;22;36.40463,293.6781;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;414.3977,-43.37161;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;478.3982,113.2284;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RGBToHSVNode;48;874.1087,430.719;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;53;933.1088,661.7191;Inherit;False;Property;_Postcount;Post count;20;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;1153.11,495.719;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RoundOpNode;52;1338.36,397.5255;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;55;1740.033,511.3951;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;58;1194.11,270.719;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;57;1182.361,237.3651;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;56;1904.71,342.7191;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WireNode;63;1173.436,-351.6851;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;50;474.1087,480.719;Inherit;False;Constant;_Float0;Float 0;13;0;Create;True;0;0;False;0;0.4545;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;49;682.1087,334.7191;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;1532.361,555.6179;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;2089.71,532.7191;Inherit;False;Constant;_Float1;Float 1;12;0;Create;True;0;0;False;0;2.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;62;1964.724,-200.7109;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CeilOpNode;99;1346.005,469.8763;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;59;2169.71,356.7191;Inherit;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;87;1128.818,1179.428;Inherit;False;Property;_Blend;Blend;18;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;96;1130.455,1108.139;Inherit;False;Property;_size;size;17;0;Create;True;0;0;False;0;0;0;-1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;80;867.5972,1107.413;Inherit;False;Property;_Color1;Color 1;19;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;95;1435.455,1134.139;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;94;1534.455,1007.139;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GradientSampleNode;112;1301.961,761.5178;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GradientNode;113;1071.961,808.5178;Inherit;False;0;2;5;1,1,1,0;1,1,1,1;0,0.001617456;1,0.560647;0,0.5956817;0.972549,0.9541771;0.972549,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;26;2254.709,-177.3944;Float;False;True;-1;2;ASEMaterialInspector;0;4;ToonShader;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;17;0;12;0
WireConnection;17;1;18;0
WireConnection;13;0;17;0
WireConnection;6;2;1;0
WireConnection;39;2;13;0
WireConnection;39;3;13;1
WireConnection;39;4;6;0
WireConnection;39;1;1;0
WireConnection;22;0;1;0
WireConnection;22;1;6;0
WireConnection;19;0;39;0
WireConnection;19;1;21;0
WireConnection;23;0;19;0
WireConnection;23;1;22;0
WireConnection;48;0;23;0
WireConnection;54;0;48;3
WireConnection;54;1;53;0
WireConnection;52;0;54;0
WireConnection;55;0;52;0
WireConnection;55;1;53;0
WireConnection;58;0;48;2
WireConnection;57;0;48;1
WireConnection;56;0;57;0
WireConnection;56;1;58;0
WireConnection;56;2;55;0
WireConnection;63;0;19;0
WireConnection;49;1;50;0
WireConnection;114;0;54;0
WireConnection;62;0;63;0
WireConnection;62;1;56;0
WireConnection;99;0;54;0
WireConnection;59;0;56;0
WireConnection;59;1;60;0
WireConnection;95;0;96;0
WireConnection;95;1;87;0
WireConnection;94;1;96;0
WireConnection;112;0;113;0
WireConnection;112;1;48;3
WireConnection;26;0;62;0
ASEEND*/
//CHKSM=FFF6744B03F42B6E27F51451C204D896A0AB8C26