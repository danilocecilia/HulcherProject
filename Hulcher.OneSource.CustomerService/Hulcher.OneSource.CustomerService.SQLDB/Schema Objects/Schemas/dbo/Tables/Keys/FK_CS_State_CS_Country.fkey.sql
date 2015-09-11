ALTER TABLE [dbo].[CS_State]  WITH CHECK ADD  CONSTRAINT [FK_CS_State_CS_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[CS_Country] ([ID])


GO
ALTER TABLE [dbo].[CS_State] CHECK CONSTRAINT [FK_CS_State_CS_Country]

