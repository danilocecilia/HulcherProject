ALTER TABLE [dbo].[CS_Employee]  WITH CHECK ADD  CONSTRAINT [FK_CS_Employee_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_Division] ([ID])


GO
ALTER TABLE [dbo].[CS_Employee] CHECK CONSTRAINT [FK_CS_Employee_CS_Division]

