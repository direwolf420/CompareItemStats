# CompareItemStats

![Icon](https://raw.githubusercontent.com/direwolf420/CompareItemStats/master/icon.png)

Compare Item Stats is a clientside mod that allows you to view differences between the currently selected item and the one you are hovering over in the tooltip.

If you hover over armors or wings, and the selected item is not the same archetype, it will compare with your currently equipped gear instead.

Supported stats:
* Damage
* Crit Chance
* Speed
* Knockback
* Fishing/Bait Power
* Pick/Axe/Hammer Power
* Restored Mana/Life
* Used Mana
* Autoswing
* Tool Range
* Class
* Wing Fly Time
* Wing Horizontal/Vertical Speed
* Defense
* Life Regeneration

If you experience issues with stats not being displayed, let me know. I did not test this with many other mods or different languages.
This does not work for most stats that accessories or armors give (consume ammo%, damage%, buff immunities etc.) as there is no tangible way to fetch this information. Hopefully, in a future update I will manage to implement such a thing.

If you want to add support for different languages, you are welcome to contribute localizations to this repo

Changelog:

v0.1.3: Added "Don't Show Hint Tooltip" toggle in the config

v0.1.2: Added localization support. Currently only english

v0.1.1.2: Fixed comparison not showing for items of the same type but different prefix

v0.1.1.1: Added workshop tags

v0.1.1: Comparison only shows up when holding "Auto Select" key (usually <LeftShift>) (See the config for details)

v0.1.0.7: Update to changes in tml

v0.1.0.6: Preemptive update to future changes in tml

v0.1.0.5: Update to changes in tml

v0.1.0.4: Update to changes in tml

v0.1.0.3: Changed Speed stat to display effective speed (combination of various parameters, not just animation time), added life regeneration

v0.1.0.2: Fixed icon, mana cost sign, autoswing on coins, equip priority, added class comparison

v0.1.0.1: Fixed armors/wings to prioritize held item if it is the same archetype