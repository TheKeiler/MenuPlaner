INSERT INTO [MenuPlanerAppAPIContext-f6bdba59-5860-415f-9c18-c3c543fbef8c].[dbo].[Ingredient]

([Name], [Description], [ReferenceUnit], [CompatibleForFructose],[CompatibleForHistamin] , [CompatibleForLactose], [CompatibleForCeliac])

VALUES

('Karotten','frisch', 'g', 1, 1, 1, 1),
('Buillon', 'in Wasser aufgelöst', 'ml', 1, 0, 1, 0),
('Zwiebel', NULL, 'Stück', 1, 0, 1, 1),
('Kokosmilch', NULL, 'ml', 1, 1, 1, 1),
('Curry', 'Pulver', 'EL', 1, 1, 1, 1),
('Chilliflocken', NULL, 'EL' ,1, 1, 1, 1),
('Salz', NULL, 'EL', 1, 1, 1, 1),
('Pfeffer', 'weiss', 'EL',1, 1, 1, 1),
('Zitronensaft', NULL, 'ml', 0, 1, 1, 1),
('Zucker', NULL, 'g', 1, 1, 1, 1),
('Haselnüsse', 'geröstet (Plätchen)', 'g', 1, 1, 1, 1),
('Rinderhackfleisch', NULL, 'g', 1, 0, 1, 1),
('Worcestersauce', NULL, 'TL', 1, 0, 0, 0),
('Pflanzenöl', 'neutral', 'EL', 1, 1, 1, 1),
('Schmelzkäse', NULL, 'Scheiben', 1, 0, 0, 0),
('Friséesalat', NULL, 'Blätter', 1, 1, 1, 1),
('Kopfsalat', NULL, 'Stück', 1, 1, 1, 1),
('Tomaten', NULL, 'Stück', 0, 0, 1, 1),
('Tomatenketchup', NULL, 'TL', 0, 0, 1, 1),
('Burgerbrötchen', NULL, 'Stück', 1, 1, 0, 0),
('Öl', 'zum braten', 'TL', 1, 1, 1, 1),
('Pouletbrust', 'längs halbiert', 'g', 1, 1, 1, 1),
('Saucen-Halbrahm', NULL, 'g', 1, 1, 0, 1),
('Zitrone', 'Bio, abgeriebene Schale', 'Stück', 0, 1, 1, 1),
('Parmesan', 'gerieben', 'g', 1, 0, 0, 1);