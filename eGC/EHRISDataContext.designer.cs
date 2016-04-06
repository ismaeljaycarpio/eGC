﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eGC
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="dbAMS")]
	public partial class EHRISDataContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertEMPLOYEE(EMPLOYEE instance);
    partial void UpdateEMPLOYEE(EMPLOYEE instance);
    partial void DeleteEMPLOYEE(EMPLOYEE instance);
    partial void InsertPOSITION(POSITION instance);
    partial void UpdatePOSITION(POSITION instance);
    partial void DeletePOSITION(POSITION instance);
    partial void InsertDEPARTMENT(DEPARTMENT instance);
    partial void UpdateDEPARTMENT(DEPARTMENT instance);
    partial void DeleteDEPARTMENT(DEPARTMENT instance);
    #endregion
		
		public EHRISDataContextDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EHRISDataContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EHRISDataContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EHRISDataContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EHRISDataContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<EMPLOYEE> EMPLOYEEs
		{
			get
			{
				return this.GetTable<EMPLOYEE>();
			}
		}
		
		public System.Data.Linq.Table<POSITION> POSITIONs
		{
			get
			{
				return this.GetTable<POSITION>();
			}
		}
		
		public System.Data.Linq.Table<DEPARTMENT> DEPARTMENTs
		{
			get
			{
				return this.GetTable<DEPARTMENT>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.EMPLOYEE")]
	public partial class EMPLOYEE : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _UserId;
		
		private int _RowId;
		
		private string _Emp_Id;
		
		private string _FirstName;
		
		private string _MiddleName;
		
		private string _LastName;
		
		private string _M_Status;
		
		private string _Gender;
		
		private System.Nullable<int> _NationalityId;
		
		private string _BirthDate;
		
		private System.Nullable<int> _Age;
		
		private string _BloodType;
		
		private string _Language;
		
		private string _ContactNo;
		
		private System.Nullable<int> _PositionId;
		
		private System.Nullable<int> _Emp_Status;
		
		private string _SubUnit;
		
		private string _JoinDate;
		
		private string _Contract_SD;
		
		private string _Contract_ED;
		
		private System.Nullable<int> _AgencyId;
		
		private System.Nullable<int> _AccountStatusId;
		
		private string _DateAccountStatusModified;
		
		private EntityRef<POSITION> _POSITION;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnRowIdChanging(int value);
    partial void OnRowIdChanged();
    partial void OnEmp_IdChanging(string value);
    partial void OnEmp_IdChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnMiddleNameChanging(string value);
    partial void OnMiddleNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnM_StatusChanging(string value);
    partial void OnM_StatusChanged();
    partial void OnGenderChanging(string value);
    partial void OnGenderChanged();
    partial void OnNationalityIdChanging(System.Nullable<int> value);
    partial void OnNationalityIdChanged();
    partial void OnBirthDateChanging(string value);
    partial void OnBirthDateChanged();
    partial void OnAgeChanging(System.Nullable<int> value);
    partial void OnAgeChanged();
    partial void OnBloodTypeChanging(string value);
    partial void OnBloodTypeChanged();
    partial void OnLanguageChanging(string value);
    partial void OnLanguageChanged();
    partial void OnContactNoChanging(string value);
    partial void OnContactNoChanged();
    partial void OnPositionIdChanging(System.Nullable<int> value);
    partial void OnPositionIdChanged();
    partial void OnEmp_StatusChanging(System.Nullable<int> value);
    partial void OnEmp_StatusChanged();
    partial void OnSubUnitChanging(string value);
    partial void OnSubUnitChanged();
    partial void OnJoinDateChanging(string value);
    partial void OnJoinDateChanged();
    partial void OnContract_SDChanging(string value);
    partial void OnContract_SDChanged();
    partial void OnContract_EDChanging(string value);
    partial void OnContract_EDChanged();
    partial void OnAgencyIdChanging(System.Nullable<int> value);
    partial void OnAgencyIdChanged();
    partial void OnAccountStatusIdChanging(System.Nullable<int> value);
    partial void OnAccountStatusIdChanged();
    partial void OnDateAccountStatusModifiedChanging(string value);
    partial void OnDateAccountStatusModifiedChanged();
    #endregion
		
		public EMPLOYEE()
		{
			this._POSITION = default(EntityRef<POSITION>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowId", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int RowId
		{
			get
			{
				return this._RowId;
			}
			set
			{
				if ((this._RowId != value))
				{
					this.OnRowIdChanging(value);
					this.SendPropertyChanging();
					this._RowId = value;
					this.SendPropertyChanged("RowId");
					this.OnRowIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Emp_Id", DbType="VarChar(50)")]
		public string Emp_Id
		{
			get
			{
				return this._Emp_Id;
			}
			set
			{
				if ((this._Emp_Id != value))
				{
					this.OnEmp_IdChanging(value);
					this.SendPropertyChanging();
					this._Emp_Id = value;
					this.SendPropertyChanged("Emp_Id");
					this.OnEmp_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(50)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiddleName", DbType="VarChar(50)")]
		public string MiddleName
		{
			get
			{
				return this._MiddleName;
			}
			set
			{
				if ((this._MiddleName != value))
				{
					this.OnMiddleNameChanging(value);
					this.SendPropertyChanging();
					this._MiddleName = value;
					this.SendPropertyChanged("MiddleName");
					this.OnMiddleNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(50)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_M_Status", DbType="VarChar(50)")]
		public string M_Status
		{
			get
			{
				return this._M_Status;
			}
			set
			{
				if ((this._M_Status != value))
				{
					this.OnM_StatusChanging(value);
					this.SendPropertyChanging();
					this._M_Status = value;
					this.SendPropertyChanged("M_Status");
					this.OnM_StatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="VarChar(50)")]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this.OnGenderChanging(value);
					this.SendPropertyChanging();
					this._Gender = value;
					this.SendPropertyChanged("Gender");
					this.OnGenderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NationalityId", DbType="Int")]
		public System.Nullable<int> NationalityId
		{
			get
			{
				return this._NationalityId;
			}
			set
			{
				if ((this._NationalityId != value))
				{
					this.OnNationalityIdChanging(value);
					this.SendPropertyChanging();
					this._NationalityId = value;
					this.SendPropertyChanged("NationalityId");
					this.OnNationalityIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthDate", DbType="VarChar(50)")]
		public string BirthDate
		{
			get
			{
				return this._BirthDate;
			}
			set
			{
				if ((this._BirthDate != value))
				{
					this.OnBirthDateChanging(value);
					this.SendPropertyChanging();
					this._BirthDate = value;
					this.SendPropertyChanged("BirthDate");
					this.OnBirthDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Age", DbType="Int")]
		public System.Nullable<int> Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this.OnAgeChanging(value);
					this.SendPropertyChanging();
					this._Age = value;
					this.SendPropertyChanged("Age");
					this.OnAgeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BloodType", DbType="VarChar(50)")]
		public string BloodType
		{
			get
			{
				return this._BloodType;
			}
			set
			{
				if ((this._BloodType != value))
				{
					this.OnBloodTypeChanging(value);
					this.SendPropertyChanging();
					this._BloodType = value;
					this.SendPropertyChanged("BloodType");
					this.OnBloodTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Language", DbType="VarChar(MAX)")]
		public string Language
		{
			get
			{
				return this._Language;
			}
			set
			{
				if ((this._Language != value))
				{
					this.OnLanguageChanging(value);
					this.SendPropertyChanging();
					this._Language = value;
					this.SendPropertyChanged("Language");
					this.OnLanguageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactNo", DbType="VarChar(50)")]
		public string ContactNo
		{
			get
			{
				return this._ContactNo;
			}
			set
			{
				if ((this._ContactNo != value))
				{
					this.OnContactNoChanging(value);
					this.SendPropertyChanging();
					this._ContactNo = value;
					this.SendPropertyChanged("ContactNo");
					this.OnContactNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionId", DbType="Int")]
		public System.Nullable<int> PositionId
		{
			get
			{
				return this._PositionId;
			}
			set
			{
				if ((this._PositionId != value))
				{
					if (this._POSITION.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPositionIdChanging(value);
					this.SendPropertyChanging();
					this._PositionId = value;
					this.SendPropertyChanged("PositionId");
					this.OnPositionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Emp_Status", DbType="Int")]
		public System.Nullable<int> Emp_Status
		{
			get
			{
				return this._Emp_Status;
			}
			set
			{
				if ((this._Emp_Status != value))
				{
					this.OnEmp_StatusChanging(value);
					this.SendPropertyChanging();
					this._Emp_Status = value;
					this.SendPropertyChanged("Emp_Status");
					this.OnEmp_StatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SubUnit", DbType="VarChar(50)")]
		public string SubUnit
		{
			get
			{
				return this._SubUnit;
			}
			set
			{
				if ((this._SubUnit != value))
				{
					this.OnSubUnitChanging(value);
					this.SendPropertyChanging();
					this._SubUnit = value;
					this.SendPropertyChanged("SubUnit");
					this.OnSubUnitChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JoinDate", DbType="VarChar(50)")]
		public string JoinDate
		{
			get
			{
				return this._JoinDate;
			}
			set
			{
				if ((this._JoinDate != value))
				{
					this.OnJoinDateChanging(value);
					this.SendPropertyChanging();
					this._JoinDate = value;
					this.SendPropertyChanged("JoinDate");
					this.OnJoinDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Contract_SD", DbType="VarChar(50)")]
		public string Contract_SD
		{
			get
			{
				return this._Contract_SD;
			}
			set
			{
				if ((this._Contract_SD != value))
				{
					this.OnContract_SDChanging(value);
					this.SendPropertyChanging();
					this._Contract_SD = value;
					this.SendPropertyChanged("Contract_SD");
					this.OnContract_SDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Contract_ED", DbType="VarChar(50)")]
		public string Contract_ED
		{
			get
			{
				return this._Contract_ED;
			}
			set
			{
				if ((this._Contract_ED != value))
				{
					this.OnContract_EDChanging(value);
					this.SendPropertyChanging();
					this._Contract_ED = value;
					this.SendPropertyChanged("Contract_ED");
					this.OnContract_EDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AgencyId", DbType="Int")]
		public System.Nullable<int> AgencyId
		{
			get
			{
				return this._AgencyId;
			}
			set
			{
				if ((this._AgencyId != value))
				{
					this.OnAgencyIdChanging(value);
					this.SendPropertyChanging();
					this._AgencyId = value;
					this.SendPropertyChanged("AgencyId");
					this.OnAgencyIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountStatusId", DbType="Int")]
		public System.Nullable<int> AccountStatusId
		{
			get
			{
				return this._AccountStatusId;
			}
			set
			{
				if ((this._AccountStatusId != value))
				{
					this.OnAccountStatusIdChanging(value);
					this.SendPropertyChanging();
					this._AccountStatusId = value;
					this.SendPropertyChanged("AccountStatusId");
					this.OnAccountStatusIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateAccountStatusModified", DbType="VarChar(50)")]
		public string DateAccountStatusModified
		{
			get
			{
				return this._DateAccountStatusModified;
			}
			set
			{
				if ((this._DateAccountStatusModified != value))
				{
					this.OnDateAccountStatusModifiedChanging(value);
					this.SendPropertyChanging();
					this._DateAccountStatusModified = value;
					this.SendPropertyChanged("DateAccountStatusModified");
					this.OnDateAccountStatusModifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="POSITION_EMPLOYEE", Storage="_POSITION", ThisKey="PositionId", OtherKey="Id", IsForeignKey=true)]
		public POSITION POSITION
		{
			get
			{
				return this._POSITION.Entity;
			}
			set
			{
				POSITION previousValue = this._POSITION.Entity;
				if (((previousValue != value) 
							|| (this._POSITION.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._POSITION.Entity = null;
						previousValue.EMPLOYEEs.Remove(this);
					}
					this._POSITION.Entity = value;
					if ((value != null))
					{
						value.EMPLOYEEs.Add(this);
						this._PositionId = value.Id;
					}
					else
					{
						this._PositionId = default(Nullable<int>);
					}
					this.SendPropertyChanged("POSITION");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.POSITION")]
	public partial class POSITION : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Position1;
		
		private System.Nullable<System.Guid> _RoleId;
		
		private System.Nullable<int> _DepartmentId;
		
		private EntitySet<EMPLOYEE> _EMPLOYEEs;
		
		private EntityRef<DEPARTMENT> _DEPARTMENT;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPosition1Changing(string value);
    partial void OnPosition1Changed();
    partial void OnRoleIdChanging(System.Nullable<System.Guid> value);
    partial void OnRoleIdChanged();
    partial void OnDepartmentIdChanging(System.Nullable<int> value);
    partial void OnDepartmentIdChanged();
    #endregion
		
		public POSITION()
		{
			this._EMPLOYEEs = new EntitySet<EMPLOYEE>(new Action<EMPLOYEE>(this.attach_EMPLOYEEs), new Action<EMPLOYEE>(this.detach_EMPLOYEEs));
			this._DEPARTMENT = default(EntityRef<DEPARTMENT>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Position", Storage="_Position1", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Position1
		{
			get
			{
				return this._Position1;
			}
			set
			{
				if ((this._Position1 != value))
				{
					this.OnPosition1Changing(value);
					this.SendPropertyChanging();
					this._Position1 = value;
					this.SendPropertyChanged("Position1");
					this.OnPosition1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleId", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				if ((this._RoleId != value))
				{
					this.OnRoleIdChanging(value);
					this.SendPropertyChanging();
					this._RoleId = value;
					this.SendPropertyChanged("RoleId");
					this.OnRoleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentId", DbType="Int")]
		public System.Nullable<int> DepartmentId
		{
			get
			{
				return this._DepartmentId;
			}
			set
			{
				if ((this._DepartmentId != value))
				{
					if (this._DEPARTMENT.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDepartmentIdChanging(value);
					this.SendPropertyChanging();
					this._DepartmentId = value;
					this.SendPropertyChanged("DepartmentId");
					this.OnDepartmentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="POSITION_EMPLOYEE", Storage="_EMPLOYEEs", ThisKey="Id", OtherKey="PositionId")]
		public EntitySet<EMPLOYEE> EMPLOYEEs
		{
			get
			{
				return this._EMPLOYEEs;
			}
			set
			{
				this._EMPLOYEEs.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DEPARTMENT_POSITION", Storage="_DEPARTMENT", ThisKey="DepartmentId", OtherKey="Id", IsForeignKey=true)]
		public DEPARTMENT DEPARTMENT
		{
			get
			{
				return this._DEPARTMENT.Entity;
			}
			set
			{
				DEPARTMENT previousValue = this._DEPARTMENT.Entity;
				if (((previousValue != value) 
							|| (this._DEPARTMENT.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._DEPARTMENT.Entity = null;
						previousValue.POSITIONs.Remove(this);
					}
					this._DEPARTMENT.Entity = value;
					if ((value != null))
					{
						value.POSITIONs.Add(this);
						this._DepartmentId = value.Id;
					}
					else
					{
						this._DepartmentId = default(Nullable<int>);
					}
					this.SendPropertyChanged("DEPARTMENT");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_EMPLOYEEs(EMPLOYEE entity)
		{
			this.SendPropertyChanging();
			entity.POSITION = this;
		}
		
		private void detach_EMPLOYEEs(EMPLOYEE entity)
		{
			this.SendPropertyChanging();
			entity.POSITION = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DEPARTMENT")]
	public partial class DEPARTMENT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Department1;
		
		private System.Nullable<System.DateTime> _CreationDate;
		
		private System.Nullable<System.DateTime> _ModifiedDate;
		
		private string _ModifiedBy;
		
		private EntitySet<POSITION> _POSITIONs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDepartment1Changing(string value);
    partial void OnDepartment1Changed();
    partial void OnCreationDateChanging(System.Nullable<System.DateTime> value);
    partial void OnCreationDateChanged();
    partial void OnModifiedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnModifiedDateChanged();
    partial void OnModifiedByChanging(string value);
    partial void OnModifiedByChanged();
    #endregion
		
		public DEPARTMENT()
		{
			this._POSITIONs = new EntitySet<POSITION>(new Action<POSITION>(this.attach_POSITIONs), new Action<POSITION>(this.detach_POSITIONs));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Department", Storage="_Department1", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Department1
		{
			get
			{
				return this._Department1;
			}
			set
			{
				if ((this._Department1 != value))
				{
					this.OnDepartment1Changing(value);
					this.SendPropertyChanging();
					this._Department1 = value;
					this.SendPropertyChanged("Department1");
					this.OnDepartment1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreationDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreationDate
		{
			get
			{
				return this._CreationDate;
			}
			set
			{
				if ((this._CreationDate != value))
				{
					this.OnCreationDateChanging(value);
					this.SendPropertyChanging();
					this._CreationDate = value;
					this.SendPropertyChanged("CreationDate");
					this.OnCreationDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModifiedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ModifiedDate
		{
			get
			{
				return this._ModifiedDate;
			}
			set
			{
				if ((this._ModifiedDate != value))
				{
					this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._ModifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModifiedBy", DbType="VarChar(50)")]
		public string ModifiedBy
		{
			get
			{
				return this._ModifiedBy;
			}
			set
			{
				if ((this._ModifiedBy != value))
				{
					this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._ModifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DEPARTMENT_POSITION", Storage="_POSITIONs", ThisKey="Id", OtherKey="DepartmentId")]
		public EntitySet<POSITION> POSITIONs
		{
			get
			{
				return this._POSITIONs;
			}
			set
			{
				this._POSITIONs.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_POSITIONs(POSITION entity)
		{
			this.SendPropertyChanging();
			entity.DEPARTMENT = this;
		}
		
		private void detach_POSITIONs(POSITION entity)
		{
			this.SendPropertyChanging();
			entity.DEPARTMENT = null;
		}
	}
}
#pragma warning restore 1591