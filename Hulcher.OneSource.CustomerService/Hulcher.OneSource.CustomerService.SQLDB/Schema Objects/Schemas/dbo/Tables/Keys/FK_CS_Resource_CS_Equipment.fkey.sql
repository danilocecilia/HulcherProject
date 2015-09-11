ALTER TABLE [dbo].[CS_Resource]  WITH CHECK ADD  CONSTRAINT [FK_CS_Resource_CS_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[CS_Equipment] ([ID])


GO
ALTER TABLE [dbo].[CS_Resource] CHECK CONSTRAINT [FK_CS_Resource_CS_Equipment]

