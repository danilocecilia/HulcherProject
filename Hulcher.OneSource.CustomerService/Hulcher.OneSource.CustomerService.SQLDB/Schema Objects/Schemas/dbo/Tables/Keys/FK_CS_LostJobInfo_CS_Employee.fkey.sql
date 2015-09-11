ALTER TABLE [dbo].[CS_LostJobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_LostJobInfo_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])


GO
ALTER TABLE [dbo].[CS_LostJobInfo] CHECK CONSTRAINT [FK_CS_LostJobInfo_CS_Employee]

