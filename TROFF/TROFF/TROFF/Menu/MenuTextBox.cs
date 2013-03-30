using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TROFF.Menu
{
    internal class MenuTextBox : MenuItem
    {
        private short _periodEvolveRepeateChar;
        private short _periodEvolveCursor;
        private short _periodEvolveCursorVisibility;

        private short _cursorIndex;

        public string Value;
        public string Label;
        private string _displayedValue;

        private bool _cursorVisible;

        private short _displayedIndexBegin, _displayedIndexEnd;

        private Vector2 _textPosition, _labelPosition;

        public MenuTextBox(string label)
        {
            NoClick = false;
            FocusOnClick = true;

            Value = "";
            Label = label;
            _displayedIndexBegin = 0;
            _displayedIndexEnd = 0;
            _displayedValue = "";

            _periodEvolveCursor = 0;
            _periodEvolveRepeateChar = 0;
            _periodEvolveCursorVisibility = 0;

            _cursorVisible = true;
            _cursorIndex = 0;
        }

        public override void SetPosition(int x, int y)
        {
            Area = new Rectangle(x, y, Textures.TextBox.Base.Width, Textures.TextBox.Base.Height);

            _textPosition = new Vector2(Area.X + Area.Width / 2,
                                        Area.Y + 6);

            _labelPosition = new Vector2(Area.X + (Area.Width - (int)Fonts.Trebuchet12.MeasureString(Label).X) / 2, Area.Y - 27);
        }

        private static char _keyToChar(Keys k)
        {
            switch (k)
            {
                case Keys.OemPeriod:
                case Keys.OemComma:
                case Keys.Decimal:
                    return '.';

                #region test-0-9

                case Keys.D0:
                case Keys.NumPad0:
                    return '0';
                case Keys.D1:
                case Keys.NumPad1:
                    return '1';
                case Keys.D2:
                case Keys.NumPad2:
                    return '2';
                case Keys.D3:
                case Keys.NumPad3:
                    return '3';
                case Keys.D4:
                case Keys.NumPad4:
                    return '4';
                case Keys.D5:
                case Keys.NumPad5:
                    return '5';
                case Keys.D6:
                case Keys.NumPad6:
                    return '6';
                case Keys.D7:
                case Keys.NumPad7:
                    return '7';
                case Keys.D8:
                case Keys.NumPad8:
                    return '8';
                case Keys.D9:
                case Keys.NumPad9:
                    return '9';

                #endregion

                #region test a-z

                case Keys.A:
                    return 'a';
                case Keys.B:
                    return 'b';
                case Keys.C:
                    return 'c';
                case Keys.D:
                    return 'd';
                case Keys.E:
                    return 'e';
                case Keys.F:
                    return 'f';
                case Keys.G:
                    return 'g';
                case Keys.H:
                    return 'h';
                case Keys.I:
                    return 'i';
                case Keys.J:
                    return 'j';
                case Keys.K:
                    return 'k';
                case Keys.L:
                    return 'l';
                case Keys.M:
                    return 'm';
                case Keys.N:
                    return 'n';
                case Keys.O:
                    return 'o';
                case Keys.P:
                    return 'p';
                case Keys.Q:
                    return 'q';
                case Keys.R:
                    return 'r';
                case Keys.S:
                    return 's';
                case Keys.T:
                    return 't';
                case Keys.U:
                    return 'u';
                case Keys.V:
                    return 'v';
                case Keys.W:
                    return 'w';
                case Keys.X:
                    return 'x';
                case Keys.Y:
                    return 'y';
                case Keys.Z:
                    return 'z';

                #endregion

                default:
                    return '$';
            }
        }

        public override void Update(GameTime gameTime)
        {
            _periodEvolveCursorVisibility++;
            _cursorVisible = _periodEvolveCursorVisibility > 40;
            if (_periodEvolveCursorVisibility == 65)
                _periodEvolveCursorVisibility = 0;

            if (!Focused || Data.PKs.GetPressedKeys().Length == 0) return;

            _periodEvolveCursorVisibility = 41;

            foreach (var pressedKey in Data.PKs.GetPressedKeys())
            {
                var inputChar = _keyToChar(pressedKey);

                if (inputChar > 96 && inputChar < 123 &&
                    (Data.PKs.GetPressedKeys().Contains(Keys.LeftShift) ||
                     Data.PKs.GetPressedKeys().Contains(Keys.RightShift)))
                    inputChar = (char)(inputChar - 32);

                if (inputChar != '$' &&
                    ((Data.Ks.IsKeyUp(pressedKey) ||
                      _periodEvolveRepeateChar == Data.MenuTextBoxPeriod) &&
                     Data.PKs.IsKeyDown(pressedKey)))
                {
                    if (_displayedIndexBegin == 0 && _displayedIndexEnd == Value.Length &&
                        Value.Length < Data.TextBoxCharsDisplayed)
                    {
                        Value = Value.Insert(_cursorIndex, inputChar.ToString());

                        if (Value.Length <= Data.TextBoxCharsDisplayed)
                            _cursorIndex++;

                        _displayedIndexEnd++;
                    }

                    else if (_cursorIndex == Data.TextBoxCharsDisplayed)
                    {
                        Value = Value.Insert(_displayedIndexBegin + _cursorIndex, inputChar.ToString());

                        _displayedIndexBegin++;
                        _displayedIndexEnd++;
                    }

                    else
                    {
                        Value = Value.Insert(_displayedIndexBegin + _cursorIndex, inputChar.ToString());

                        _cursorIndex++;
                    }

                    _periodEvolveRepeateChar = 0;
                }

                else
                {
                    if ((Data.Ks.IsKeyUp(Keys.Right) ||
                         _periodEvolveCursor == Data.MenuTextBoxCursorPeriod) &&
                        Data.PKs.IsKeyDown(Keys.Right))
                    {
                        if (_cursorIndex < _displayedIndexEnd - _displayedIndexBegin)
                            _cursorIndex++;

                        else if (_displayedIndexEnd < Value.Length - 1)
                        {
                            _displayedIndexBegin++;
                            _displayedIndexEnd++;
                        }

                        _periodEvolveCursor = 0;
                    }

                    if ((Data.Ks.IsKeyUp(Keys.Left) ||
                         _periodEvolveCursor == Data.MenuTextBoxCursorPeriod) &&
                        Data.PKs.IsKeyDown(Keys.Left))
                    {
                        if (_cursorIndex > 0)
                            _cursorIndex--;

                        else if (_displayedIndexBegin > 0)
                        {
                            _displayedIndexBegin--;
                            _displayedIndexEnd--;
                        }

                        _periodEvolveCursor = 0;
                    }

                    if ((Data.Ks.IsKeyUp(Keys.Back) ||
                         _periodEvolveRepeateChar == Data.MenuTextBoxPeriod) &&
                        Data.PKs.IsKeyDown(Keys.Back) && Value.Length > 0)
                    {
                        if (_displayedIndexBegin == 0 && _displayedIndexEnd == Value.Length &&
                            Value.Length <= Data.TextBoxCharsDisplayed && _cursorIndex > 0)
                        {
                            Value = Value.Remove(_cursorIndex - 1, 1);

                            _cursorIndex--;

                            _displayedIndexEnd--;
                        }

                        else if (_cursorIndex != 0)
                        {
                            Value = Value.Remove(_displayedIndexBegin + _cursorIndex - 1, 1);

                            _displayedIndexEnd--;
                            _displayedIndexBegin--;
                        }

                        _periodEvolveRepeateChar = 0;
                    }
                }

                if (Data.Ks.IsKeyDown(Keys.Left) || Data.Ks.IsKeyDown(Keys.Right))
                    _periodEvolveCursor++;

                if (Data.PKs.IsKeyDown(pressedKey))
                    _periodEvolveRepeateChar++;
            }

            _displayedValue = Value.Substring(_displayedIndexBegin, _displayedIndexEnd - _displayedIndexBegin);

            _textPosition.X = Area.X + (Area.Width - (int)Fonts.Trebuchet12.MeasureString(_displayedValue).X) / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Focused ? Textures.TextBox.Focus : Textures.TextBox.Base, Area,
                             Color.White);

            spriteBatch.DrawString(Fonts.Trebuchet12, _displayedValue, _textPosition, new Color(0, 255, 210, Focused ? 100 : 70));

            if (_cursorVisible && Focused)
                spriteBatch.Draw(Textures.Cursor,
                                 new Vector2(
                                     _textPosition.X +
                                     _cursorIndex *
                                     (_cursorIndex == 0
                                          ? 0
                                          : (Fonts.Trebuchet12.MeasureString(_displayedValue).X /
                                             _displayedValue.Length)), _textPosition.Y - 6), Color.White);

            spriteBatch.DrawString(Fonts.Trebuchet12, Label, _labelPosition, new Color(0, 255, 210));
        }
    }
}
