﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace MapDesigner.UI
{
    public static class InterfaceUtility
    {
        public static float AnglePicker(Listing_Standard listing, float val, string label, int spinnerType = 1, bool fullCircle = false, bool snap = true)
        {
            Rect rect = new Rect(listing.GetRect(44f));
            Widgets.DrawHighlightIfMouseover(rect);
            Rect leftSide = rect;
            Rect rightSide = rect;
            leftSide.xMax -= 0.76f * rect.width;
            rightSide.xMin += 0.26f * rect.width;
            Rect sliderRect = rightSide;
            sliderRect.yMin += 16f;

            Widgets.Label(leftSide, String.Format("{0}: {1}°", label, val));

            Rect spinnerRect = new Rect(leftSide);
            spinnerRect.xMax -= 10f;
            spinnerRect.xMin = spinnerRect.xMax - 44f;

            string texPath = String.Format("GUI/ZMD_spinner{0}", spinnerType);
            Texture2D spinner = ContentFinder<Texture2D>.Get(texPath, true);
            Widgets.DrawTextureRotated(spinnerRect, spinner, val);

            float result = val;
            float max = fullCircle ? 36f : 18f;
            result = 10f * (float)Math.Round(GUI.HorizontalSlider(sliderRect, val * 0.1f, 0f, max));
            return result;
        }


        public static bool SizedTextButton(Listing_Standard listing, string label, float width = -1f, bool centered = false)
        {
            Rect rect = new Rect(listing.GetRect(30f));
            if(width == -1f)
            {
                width = Text.CalcSize(label).x + 50f;
            }
            rect.width = width;
            if(centered)
            {
                rect.x += 0.5f * (listing.ColumnWidth -rect.width);
            }
            bool result = Widgets.ButtonText(rect, label, true, false, true);
            listing.Gap(listing.verticalSpacing);
            return result;
        }


        public static float LabeledSlider(Listing_Standard listing, float val, float min, float max, string label, string leftLabel = null, string rightLabel = null, string tooltip = null)
        {
            Rect rect = new Rect(listing.GetRect(44f));

            Widgets.DrawHighlightIfMouseover(rect);

            Rect leftSide = rect;
            Rect rightSide = rect;
            leftSide.xMax -= 0.76f * rect.width;
            rightSide.xMin += 0.26f * rect.width;
            float result = val;

            Widgets.Label(leftSide, label);
            Rect sliderRect = rightSide;
            sliderRect.yMin += 16f;

            result = GUI.HorizontalSlider(sliderRect, val, min, max);

            TextAnchor anchor = Text.Anchor;
            GameFont font = Text.Font;
            Text.Font = GameFont.Tiny;

            if (!leftLabel.NullOrEmpty())
            {
                Text.Anchor = TextAnchor.UpperLeft;
                Widgets.Label(rightSide, leftLabel);
            }
            if (!rightLabel.NullOrEmpty())
            {
                Text.Anchor = TextAnchor.UpperRight;
                Widgets.Label(rightSide, rightLabel);
            }

            Text.Anchor = anchor;
            Text.Font = font;

            if (tooltip != null)
            {
                TooltipHandler.TipRegion(rect, tooltip);
            }
            return result;

        }


        public static void LabeledIntRange(Listing_Standard listing, ref IntRange range, int min, int max, string label, string tooltip = null)
        {
            Rect rect = new Rect(listing.GetRect(44f));
            Widgets.DrawHighlightIfMouseover(rect);

            Rect innerRect = rect;
            innerRect.yMin += 8f;
            innerRect.yMax -= 8f;

            Rect leftSide = innerRect;
            Rect rightSide = innerRect;
            leftSide.xMax -= 0.76f * innerRect.width;
            rightSide.xMin += 0.26f * innerRect.width;

            Widgets.Label(leftSide, label);

            Widgets.IntRange(rightSide, (int)listing.CurHeight, ref range, min, max, null, 0);

            if (tooltip != null)
            {
                TooltipHandler.TipRegion(rect, tooltip);
            }
        }


        public static string FormatLabel(string label, string desc)
        {
            return String.Format("{0}: {1}", label.Translate(), desc.Translate());
        }

    }
}
