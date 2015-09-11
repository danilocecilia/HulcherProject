ALTER TABLE [dbo].[CS_LostJobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_LostJobInfo_CS_Competitor] FOREIGN KEY([CompetitorID])
REFERENCES [dbo].[CS_Competitor] ([ID])


GO
ALTER TABLE [dbo].[CS_LostJobInfo] CHECK CONSTRAINT [FK_CS_LostJobInfo_CS_Competitor]

