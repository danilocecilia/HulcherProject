ALTER TABLE [dbo].[CS_LocationInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocationInfo_CS_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[CS_Country] ([ID])


GO
ALTER TABLE [dbo].[CS_LocationInfo] CHECK CONSTRAINT [FK_CS_LocationInfo_CS_Country]

