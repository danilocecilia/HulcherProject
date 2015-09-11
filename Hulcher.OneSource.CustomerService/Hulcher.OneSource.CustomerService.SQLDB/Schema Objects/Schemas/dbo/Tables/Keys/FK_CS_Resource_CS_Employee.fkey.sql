ALTER TABLE [dbo].[CS_Resource]  WITH CHECK ADD  CONSTRAINT [FK_CS_Resource_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])


GO
ALTER TABLE [dbo].[CS_Resource] CHECK CONSTRAINT [FK_CS_Resource_CS_Employee]

