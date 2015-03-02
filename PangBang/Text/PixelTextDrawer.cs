using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework;
using PangBang.Draw;

namespace PangBang.Text
{
    public class PixelTextDrawer : ITextDrawer
    {
        private readonly IDrawer _drawer;
        private readonly IDictionary<string, PixelTextMap> _vectorMapDictionary;

        public int Width
        {
            get { return 7; }
        }

        public int Height
        {
            get { return 9; }
        }

        public PixelTextDrawer(IDrawer drawer)
        {
            _drawer = drawer;
            _vectorMapDictionary = InitializePixelMap();
        }

        private PixelTextMap TextPixelPlot(string text, int size)
        {
            var width = 0;
            var height = 0;
            var pixels = new List<Vector2>();

            foreach (
                var pixelPlot in
                    text.Select(letter => _vectorMapDictionary[letter.ToString(CultureInfo.InvariantCulture).ToUpperInvariant()]))
            {
                width += pixelPlot.Width * size;
                height = Math.Max(height, pixelPlot.Height * size);
                pixels.AddRange(pixelPlot.Pixels);
            }

            return new PixelTextMap
            {
                Width = width,
                Height = height,
                Pixels = pixels
            };
        }

        public void DrawText(string text, Vector2 position, int size,
            Color color)
        {
            var basePosition = position;

            foreach (var number in text.ToArray())
            {
                var positionMap = GetPixelPlot(number.ToString(CultureInfo.InvariantCulture));
                foreach (var pixelPosition in positionMap.Pixels)
                {
                    _drawer.Draw(basePosition + pixelPosition * size, color);
                }

                basePosition += new Vector2(Width, 0) * size;
            }
        }

        public void DrawTextCentered(string text, Vector2 position, int size,
            Color color)
        {
            var textPixelPlot = TextPixelPlot(text, size);
            var offSet = new Vector2(textPixelPlot.Width / 2.0f, textPixelPlot.Height / 2.0f);
            DrawText(text, position - offSet, size, color);
        }

        private PixelTextMap GetPixelPlot(string key)
        {
            return _vectorMapDictionary[key.ToUpper()];
        }

        private IDictionary<string, PixelTextMap> InitializePixelMap()
        {
            return new Dictionary<string, PixelTextMap>
            {
                {
                    " ",
                    new PixelTextMap
                    {
                        Width = 3,
                        Height = 9,
                        Pixels = new List<Vector2>()
                    }
                },
                {
                    "-",
                    new PixelTextMap
                    {
                        Width = 5,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 4),
                            new Vector2(2, 4),
                            new Vector2(3, 4)
                        }
                    }
                },
                {
                    ".",
                    new PixelTextMap
                    {
                        Width = 3,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 7)
                        }
                    }
                },
                {
                    ",",
                    new PixelTextMap
                    {
                        Width = 4,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 8),
                            new Vector2(2, 7)
                        }
                    }
                },
                {
                    "!",
                    new PixelTextMap
                    {
                        Width = 3,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 7),
                        }
                    }

                },
                {
                    "?",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(2, 1),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }

                },
                {
                    "A",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 2),
                            new Vector2(2, 5),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(4, 2),
                            new Vector2(4, 5),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }

                    }
                },
                {
                    "B",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "C",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "D",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "E",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "F",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                        }
                    }
                },
                {
                    "G",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "H",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 4),
                            new Vector2(3, 4),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "I",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 2),
                            new Vector2(3, 3),
                            new Vector2(3, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "J",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "K",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 4),
                            new Vector2(3, 3),
                            new Vector2(3, 5),
                            new Vector2(4, 2),
                            new Vector2(4, 6),
                            new Vector2(5, 1),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "L",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 7),
                            new Vector2(3, 7),
                            new Vector2(4, 7),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "M",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 2),
                            new Vector2(3, 3),
                            new Vector2(4, 2),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "N",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 3),
                            new Vector2(3, 4),
                            new Vector2(4, 5),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "O",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "P",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }
                },
                {
                    "Q",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 6),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "R",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "S",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "T",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(2, 1),
                            new Vector2(3, 1),
                            new Vector2(3, 2),
                            new Vector2(3, 3),
                            new Vector2(3, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(5, 1),
                        }
                    }
                },
                {
                    "U",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 7),
                            new Vector2(3, 7),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "V",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 6),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "W",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 7),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "X",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 6),
                            new Vector2(2, 3),
                            new Vector2(2, 5),
                            new Vector2(3, 4),
                            new Vector2(4, 3),
                            new Vector2(4, 5),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 6),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "Y",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(2, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }
                },
                {
                    "Z",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 6),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 5),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 3),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "1",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 2),
                            new Vector2(3, 3),
                            new Vector2(3, 4),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 7),
                            new Vector2(5, 7)
                        }
                    }
                },
                {
                    "2",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 7),
                            new Vector2(2, 1),
                            new Vector2(2, 6),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 7),
                        }
                    }
                },
                {
                    "3",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "4",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(2, 3),
                            new Vector2(2, 5),
                            new Vector2(3, 2),
                            new Vector2(3, 5),
                            new Vector2(4, 1),
                            new Vector2(4, 2),
                            new Vector2(4, 3),
                            new Vector2(4, 4),
                            new Vector2(4, 5),
                            new Vector2(4, 6),
                            new Vector2(4, 7),
                            new Vector2(5, 5),
                        }
                    }
                },
                {
                    "5",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 3),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 3),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 3),
                            new Vector2(4, 7),
                            new Vector2(5, 1),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "6",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "7",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 1),
                            new Vector2(2, 1),
                            new Vector2(3, 1),
                            new Vector2(3, 5),
                            new Vector2(3, 6),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(5, 1),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                        }
                    }
                },
                {
                    "8",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "9",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 4),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 4),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 4),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
                {
                    "0",
                    new PixelTextMap
                    {
                        Width = 7,
                        Height = 9,
                        Pixels = new List<Vector2>
                        {
                            new Vector2(1, 2),
                            new Vector2(1, 3),
                            new Vector2(1, 4),
                            new Vector2(1, 5),
                            new Vector2(1, 6),
                            new Vector2(2, 1),
                            new Vector2(2, 7),
                            new Vector2(3, 1),
                            new Vector2(3, 7),
                            new Vector2(4, 1),
                            new Vector2(4, 7),
                            new Vector2(5, 2),
                            new Vector2(5, 3),
                            new Vector2(5, 4),
                            new Vector2(5, 5),
                            new Vector2(5, 6)
                        }
                    }
                },
            };
        }
    }
}