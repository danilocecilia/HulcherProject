ALTER TABLE CS_ResourceAllocationDetails
ADD IsSubContractor bit null

ALTER TABLE CS_ResourceAllocationDetails
ADD SubContractorInfo varchar(300) null

ALTER TABLE CS_ResourceAllocationDetails
ADD FieldPO varchar(20) null
