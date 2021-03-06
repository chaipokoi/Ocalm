

// ---- AngelCode BmFont XML serializer ----------------------
// ---- By DeadlyDan @ deadlydan@gmail.com -------------------
// ---- There's no license restrictions, use as you will. ----
// ---- Credits to http://www.angelcode.com/ -----------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoreFork
{
	[Serializable]
	[XmlRoot ( "font" )]
	public class FontFile
	{
		[XmlElement ( "info" )]
		public FontInfo Info
		{
			get;
			set;
		}

		[XmlElement ( "common" )]
		public FontCommon Common
		{
			get;
			set;
		}

		[XmlArray ( "pages" )]
		[XmlArrayItem ( "page" )]
		public List<FontPage> Pages
		{
			get;
			set;
		}

		[XmlArray ( "chars" )]
		[XmlArrayItem ( "char" )]
		public List<FontChar> Chars
		{
			get;
			set;
		}

		[XmlArray ( "kernings" )]
		[XmlArrayItem ( "kerning" )]
		public List<FontKerning> Kernings
		{
			get;
			set;
		}
	}

	[Serializable]
	public class FontInfo
	{
		[XmlAttribute ( "face" )]
		public String Face
		{
			get;
			set;
		}

		[XmlAttribute ( "size" )]
		public Int32 Size
		{
			get;
			set;
		}

		[XmlAttribute ( "bold" )]
		public Int32 Bold
		{
			get;
			set;
		}

		[XmlAttribute ( "italic" )]
		public Int32 Italic
		{
			get;
			set;
		}

		[XmlAttribute ( "charset" )]
		public String CharSet
		{
			get;
			set;
		}

		[XmlAttribute ( "unicode" )]
		public Int32 Unicode
		{
			get;
			set;
		}

		[XmlAttribute ( "stretchH" )]
		public Int32 StretchHeight
		{
			get;
			set;
		}

		[XmlAttribute ( "smooth" )]
		public Int32 Smooth
		{
			get;
			set;
		}

		[XmlAttribute ( "aa" )]
		public Int32 SuperSampling
		{
			get;
			set;
		}

		private Rectangle _Padding;
		[XmlAttribute ( "padding" )]
		public String Padding
		{
			get
			{
				return _Padding.X + "," + _Padding.Y + "," + _Padding.Width + "," + _Padding.Height;
			}
			set
			{
				String[] padding = value.Split ( ',' );
				_Padding = new Rectangle ( Convert.ToInt32 ( padding[0] ), Convert.ToInt32 ( padding[1] ), Convert.ToInt32 ( padding[2] ), Convert.ToInt32 ( padding[3] ) );
			}
		}

		private Point _Spacing;
		[XmlAttribute ( "spacing" )]
		public String Spacing
		{
			get
			{
				return _Spacing.X + "," + _Spacing.Y;
			}
			set
			{
				String[] spacing = value.Split ( ',' );
				_Spacing = new Point ( Convert.ToInt32 ( spacing[0] ), Convert.ToInt32 ( spacing[1] ) );
			}
		}

		[XmlAttribute ( "outline" )]
		public Int32 OutLine
		{
			get;
			set;
		}
	}

	[Serializable]
	public class FontCommon
	{
		[XmlAttribute ( "lineHeight" )]
		public Int32 LineHeight
		{
			get;
			set;
		}

		[XmlAttribute ( "base" )]
		public Int32 Base
		{
			get;
			set;
		}

		[XmlAttribute ( "scaleW" )]
		public Int32 ScaleW
		{
			get;
			set;
		}

		[XmlAttribute ( "scaleH" )]
		public Int32 ScaleH
		{
			get;
			set;
		}

		[XmlAttribute ( "pages" )]
		public Int32 Pages
		{
			get;
			set;
		}

		[XmlAttribute ( "packed" )]
		public Int32 Packed
		{
			get;
			set;
		}

		[XmlAttribute ( "alphaChnl" )]
		public Int32 AlphaChannel
		{
			get;
			set;
		}

		[XmlAttribute ( "redChnl" )]
		public Int32 RedChannel
		{
			get;
			set;
		}

		[XmlAttribute ( "greenChnl" )]
		public Int32 GreenChannel
		{
			get;
			set;
		}

		[XmlAttribute ( "blueChnl" )]
		public Int32 BlueChannel
		{
			get;
			set;
		}
	}

	[Serializable]
	public class FontPage
	{
		[XmlAttribute ( "id" )]
		public Int32 ID
		{
			get;
			set;
		}

		[XmlAttribute ( "file" )]
		public String File
		{
			get;
			set;
		}
	}

	[Serializable]
	public class FontChar
	{
		[XmlAttribute ( "id" )]
		public Int32 ID
		{
			get;
			set;
		}

		[XmlAttribute ( "x" )]
		public Int32 X
		{
			get;
			set;
		}

		[XmlAttribute ( "y" )]
		public Int32 Y
		{
			get;
			set;
		}

		[XmlAttribute ( "width" )]
		public Int32 Width
		{
			get;
			set;
		}

		[XmlAttribute ( "height" )]
		public Int32 Height
		{
			get;
			set;
		}

		[XmlAttribute ( "xoffset" )]
		public Int32 XOffset
		{
			get;
			set;
		}

		[XmlAttribute ( "yoffset" )]
		public Int32 YOffset
		{
			get;
			set;
		}

		[XmlAttribute ( "xadvance" )]
		public Int32 XAdvance
		{
			get;
			set;
		}

		[XmlAttribute ( "page" )]
		public Int32 Page
		{
			get;
			set;
		}

		[XmlAttribute ( "chnl" )]
		public Int32 Channel
		{
			get;
			set;
		}
	}

	[Serializable]
	public class FontKerning
	{
		[XmlAttribute ( "first" )]
		public Int32 First
		{
			get;
			set;
		}

		[XmlAttribute ( "second" )]
		public Int32 Second
		{
			get;
			set;
		}

		[XmlAttribute ( "amount" )]
		public Int32 Amount
		{
			get;
			set;
		}
	}

	public class FontLoader
	{
		public static FontFile Load ( string filename )
		{
			string v = Path.Combine(Core.content.RootDirectory, filename);
			using (Stream stream = TitleContainer.OpenStream (v)) {
				XmlSerializer deserializer = new XmlSerializer (typeof(FontFile));
				FontFile file = (FontFile)deserializer.Deserialize (stream);
				stream.Close ();
				return file;
			}
		}
	}

	public class FontRenderer
	{
		public FontRenderer (FontFile fontFile, Texture2D fontTexture)
		{
			_fontFile = fontFile;
			_texture = fontTexture;
			_characterMap = new Dictionary<char, FontChar>();

			foreach(var fontCharacter in _fontFile.Chars)
			{
				char c = (char)fontCharacter.ID;
				_characterMap.Add(c, fontCharacter);
			}
		}

		private Dictionary<char, FontChar> _characterMap;
		private FontFile _fontFile;
		private Texture2D _texture;
		public void DrawText(SpriteBatch spriteBatch, int x, int y, string text)
		{
			int dx = x;
			int dy = y;
			foreach(char c in text)
			{
				FontChar fc;
				if(_characterMap.TryGetValue(c, out fc))
				{
					var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
					var position = new Vector2(dx + fc.XOffset, dy + fc.YOffset);

					spriteBatch.Draw(_texture, position, sourceRectangle, Color.White);
					dx += fc.XAdvance;
				}
			}
		}
		public void DrawText(SpriteBatch spriteBatch, int x, int y, string text,Color color,float fontsize)
		{
			int dx = x;
			int dy = y;
			foreach(char c in text)
			{
				FontChar fc;
				if(_characterMap.TryGetValue(c, out fc))
				{
					var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
					var position = new Vector2(dx + fc.XOffset*fontsize, dy + fc.YOffset*fontsize);

					spriteBatch.Draw(_texture, position, sourceRectangle, color,0F,Vector2.Zero,fontsize,SpriteEffects.None,0F);
					dx += (int)(fc.XAdvance*fontsize);
				}
			}
		}

		public Vector2 MeasureString(string text)
		{
			int w=0;
			int h=0;
			foreach(char c in text)
			{
				FontChar fc;
				if(_characterMap.TryGetValue(c, out fc))
				{
//					var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
					w+=fc.XOffset+fc.XAdvance;
					if(fc.YOffset>h)
						h+=fc.YOffset;
				}
			}
			return new Vector2(w,h);
		}
	}
}
