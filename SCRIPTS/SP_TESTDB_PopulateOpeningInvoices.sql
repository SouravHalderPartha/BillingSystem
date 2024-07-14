USE [CODE_TEST]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('SP_TESTDB_PopulateOpeningInvoices'))
   exec('CREATE PROCEDURE [dbo].[SP_TESTDB_PopulateOpeningInvoices] AS BEGIN SET NOCOUNT ON; END')
GO


/****** Object:  StoredProcedure [dbo].[SP_TESTDB_PopulateOpeningInvoices]    Script Date: 7/14/2024 1:36:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[SP_TESTDB_PopulateOpeningInvoices] 
AS
BEGIN

-- Exec SP_TESTDB_PopulateOpeningInvoice

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON
	DECLARE 
	    @LastBillingStartDate DateTime,
		@BillingStartDate DateTime,
		@BillingEndDate DateTime,
		@BillingDurationDays int,
		@DaysInMonths int,
		@BillingYear int,
		@BillingMonth int;
		

		SET @LastBillingStartDate = (SELECT MAX(BillingStartDate) FROM BillInvoices);
		SET @BillingStartDate = (DATEADD(MONTH,1,@LastBillingStartDate ));
		SET @BillingStartDate = DATEFROMPARTS(YEAR(@BillingStartDate), MONTH(@BillingStartDate), 1);
		SET @BillingEndDate = EOMONTH(@BillingStartDate);
		SET @BillingYear = Year(@BillingStartDate);
	    SET @BillingMonth = Month(@BillingStartDate);
		SET @DaysInMonths = DAY(@BillingEndDate);
		SET @BillingDurationDays = @DaysInMonths;
    

	--print @LastBillingStartDate ;
	--print @BillingStartDate;
	--print @BillingEndDate;

	--print CONVERT(NVARCHAR, @BillingStartDate, 121);
	--print CONVERT(NVARCHAR, @BillingEndDate, 121);

	     --SELECT * from #WFOpeningInvoice Where  row=1
	 

  --SELECT 
  --    t.Id,
  --    t.ClientId,
	 -- t.LinkServiceId,
  --    t.BillingStartDate,
  --    @BillingEndDate BillingEndDate,
  --    @BillingDurationDays as BillingDurationDays,
  --    @DaysInMonths as DaysInMonths,
  --    @BillingYear as BillingYear,
  --    @BillingMonth as BillingMonth,
  --    t.Rate,
	 -- t.CapacityQty,
  --    BillAmount = (ISNULL(Rate, 0) * ISNULL(CapacityQty, 0) * ISNULL(BillingDurationDays, 0)) / ISNULL(DaysInMonths, 0) 
      

  --into #WFOpeningInvoices 
  --FROM BillInvoices AS t 
    
	 --Delete t1 from #WFOpeningInvoices t1 JOIN #WFOpeningInvoices t2 ON  t1.LinkServiceId=t2.LinkServiceId AND t1.BillingStartDate < t2.BillingStartDate
	 --Update #WFOpeningInvoices SET BillingStartDate = @BillingStartDate;
		

     SELECT 
      t.Id,
      t.ClientId,
	  t.LinkServiceId,
      @BillingStartDate as BillingStartDate,
      @BillingEndDate BillingEndDate,
      @BillingDurationDays as BillingDurationDays,
      @DaysInMonths as DaysInMonths,
      @BillingYear as BillingYear,
      @BillingMonth as BillingMonth,
      t.Rate,
	  t.CapacityQty,
      BillAmount = (ISNULL(Rate, 0) * ISNULL(CapacityQty, 0) * ISNULL(BillingDurationDays, 0)) / ISNULL(DaysInMonths, 0) ,
      ROW_NUMBER() OVER (PARTITION BY t.LinkServiceId ORDER BY BillingStartDate DESC) AS row

  into #WFOpeningInvoice
  FROM BillInvoices AS t
    

	 INSERT INTO [Test Db].[dbo].[OpeningInvoices] (
      
      ClientId,
      LinkServiceId,
      BillingStartDate,
      BillingEndDate,
      BillingDurationDays,
      DaysInMonths,
      BillingYear,
      BillingMonth,
      Rate,
      CapacityQty,
      BillAmount
  )
  SELECT ClientId,LinkServiceId,
      BillingStartDate,
      BillingEndDate,
      BillingDurationDays,
      DaysInMonths,
      BillingYear,
      BillingMonth,
      Rate,
      CapacityQty,
      BillAmount FROM #WFOpeningInvoice Where  row=1  ORDER BY ClientId ASC ;

	  drop table #WFOpeningInvoice
	  --SELECT * from [Test Db].[dbo].[OpeningInvoices]
	  --Truncate table [Test Db].[dbo].[OpeningInvoices]
     --SELECT * from #WFOpeningInvoice ORDER BY ClientId ASC 


	  

	  COMMIT TRANSACTION
	SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH
END


