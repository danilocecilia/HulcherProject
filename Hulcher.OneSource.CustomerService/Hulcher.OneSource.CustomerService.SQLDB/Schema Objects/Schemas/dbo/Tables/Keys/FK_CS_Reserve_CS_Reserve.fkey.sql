ALTER TABLE [dbo].[CS_Reserve]  WITH CHECK ADD  CONSTRAINT [FK_CS_Reserve_CS_Reserve] FOREIGN KEY([EquipmentTypeID])
REFERENCES [dbo].[CS_EquipmentType] ([ID])


GO
ALTER TABLE [dbo].[CS_Reserve] CHECK CONSTRAINT [FK_CS_Reserve_CS_Reserve]

