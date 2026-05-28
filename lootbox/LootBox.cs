using Godot;
using System;
using NfcReaderLib;
using NfcReaderLib.Interfaces;

public partial class LootBox : Node2D
{
	[Export] private AnimatedSprite2D lootBoxSprite;
	[Export] private Sprite2D prizeSprite;
	[Export] private Texture2D[] possiblePrizes;
	private readonly Random rnd = new();
	private Boolean isOpened = false;
	private INfcReader reader;

	public override void _Ready()
	{
		lootBoxSprite.Play("Idle");

		lootBoxSprite.AnimationFinished += OnAnimationFinished;

		reader = NfcReaderFactory.Create();

		GD.Print($"NFC Available: {reader.IsAvailable}");

		if (reader.IsAvailable)
		{
			reader.OnTagRead += OnTagRead;

			reader.StartReading();
		}
	}

	private void OpenLootBox()
	{
		if (isOpened)
		{
			isOpened = false;
			prizeSprite.Visible = false;
			return;
		}

		isOpened = true;

		lootBoxSprite.Play("Open");
	}

	private void OnAnimationFinished()
	{
		if (lootBoxSprite.Animation == "Open")
		{
			GiveRandomPrize();
		}
	}

	private void GiveRandomPrize()
	{
		Int32 index = rnd.Next(0, possiblePrizes.Length);

		prizeSprite.Texture = possiblePrizes[index];
		prizeSprite.Visible = true;
	}

	private void OnTagRead(String uid)
	{
		GD.Print($"NFC UID: {uid}");

		CallDeferred(nameof(OpenLootBox));
	}
}
