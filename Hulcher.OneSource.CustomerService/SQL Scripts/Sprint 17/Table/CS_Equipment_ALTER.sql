ALTER TABLE CS_Equipment
add Seasonal bit not null

go

ALTER TABLE [dbo].[CS_Equipment] ADD  CONSTRAINT [DF_CS_Equipment_Seasonal]  DEFAULT ((0)) FOR [Seasonal]
GO
