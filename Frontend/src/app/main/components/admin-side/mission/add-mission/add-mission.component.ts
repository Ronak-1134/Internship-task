import { NgFor, NgIf } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { MissionService } from 'src/app/main/services/mission.service';
import { CommonService } from 'src/app/main/services/common.service';
import { APP_CONFIG } from 'src/app/main/configs/environment.config';
import { HeaderComponent } from '../../header/header.component';
import { SidebarComponent } from '../../sidebar/sidebar.component';
import { Subscription } from 'rxjs';
import { error } from 'jquery';
import { MissionTheme } from 'src/app/main/interfaces/common.interface';

@Component({
  selector: 'app-add-mission',
  standalone: true,
  imports: [HeaderComponent, SidebarComponent, ReactiveFormsModule, NgIf, NgFor],
  templateUrl: './add-mission.component.html',
  styleUrls: ['./add-mission.component.css']
})
export class AddMissionComponent implements OnInit, OnDestroy {
  addMissionForm: FormGroup;
  endDateDisabled: boolean = true;
  regDeadlineDisabled: boolean = true;
  formValid: boolean;
  countryList: any[] = [];
  cityList: any[] = [];
  missionThemeList: any[] = [];
  missionSkillList: any[] = [];
  formData = new FormData();
  imageListArray: any[] = [];
  private unsubscribe: Subscription[] = [];

  constructor(
    private _fb: FormBuilder,
    private _service: MissionService,
    private _commonService: CommonService,
    private _router: Router,
    private _toast: NgToastService
  ) { }

  ngOnInit(): void {
    this.addMissionFormValid();
    this.setStartDate();
    this.getCountryList();
    this.getMissionSkillList();
    this.getMissionThemeList();
  }

  addMissionFormValid() {
    this.addMissionForm = this._fb.group({
      missionOrganisationName: [null, Validators.compose([Validators.required])],
      missionOrganisationDetail: [null, Validators.compose([Validators.required])],
      missionType: [null, Validators.compose([Validators.required])],
      countryId: [null, Validators.compose([Validators.required])],
      cityId: [null, Validators.compose([Validators.required])],
      missionTitle: [null, Validators.compose([Validators.required])],
      missionDescription: [null, Validators.compose([Validators.required])],
      startDate: [null, Validators.compose([Validators.required])],
      endDate: [null, Validators.compose([Validators.required])],
      missionThemeId: [null, Validators.compose([Validators.required])],
      missionSkillId: [null, Validators.compose([Validators.required])],
      missionImages: [null, Validators.compose([Validators.required])],
      totalSheets: [null, Validators.compose([Validators.required])]
    });
  }

  get countryId() { return this.addMissionForm.get('countryId') as FormControl; }
  get cityId() { return this.addMissionForm.get('cityId') as FormControl; }
  get missionTitle() { return this.addMissionForm.get('missionTitle') as FormControl; }
  get missionDescription() { return this.addMissionForm.get('missionDescription') as FormControl; }
  get startDate() { return this.addMissionForm.get('startDate') as FormControl; }
  get endDate() { return this.addMissionForm.get('endDate') as FormControl; }
  get missionThemeId() { return this.addMissionForm.get('missionThemeId') as FormControl; }
  get missionSkillId() { return this.addMissionForm.get('missionSkillId') as FormControl; }
  get missionImages() { return this.addMissionForm.get('missionImages') as FormControl; }
  get totalSheets() { return this.addMissionForm.get('totalSheets') as FormControl; }


  get missionOrganisationName() { return this.addMissionForm.get('missionOrganisationName') as FormControl; }
  get missionOrganisationDetail() { return this.addMissionForm.get('missionOrganisationDetail') as FormControl; }
  get missionType() { return this.addMissionForm.get('missionType') as FormControl; }

  setStartDate() {
    const today = new Date();
    const todayString = today.toISOString().split('T')[0];

    this.addMissionForm.patchValue({
      startDate: todayString
    });
    this.endDateDisabled = false;
    this.regDeadlineDisabled = false;
  }

  getCountryList() {
    const countryListSubscription = this._commonService.countryList().subscribe((data: any) => {
      if (data.result == 1) {
        this.countryList = data.data;
      } else {
        this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration });
      }
    });
    this.unsubscribe.push(countryListSubscription);
  }

  getCityList(countryId: any) {
    countryId = countryId.target.value;
    const cityListSubscription = this._commonService.cityList(countryId).subscribe((data: any) => {
      if (data.result == 1) {
        this.cityList = data.data;
      } else {
        this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration });
      }
    });
    this.unsubscribe.push(cityListSubscription);
  }

  getMissionSkillList() {
    const getMissionSkillListSubscription = this._service.getMissionSkillList().subscribe({
      next: (data: any) => {
        if (data.result == 1) {
          this.missionSkillList = data.data;
        } else {
          this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration });
        }
      },
      error: (err) => {
        this._toast.error({ detail: "ERROR", summary: err.message, duration: APP_CONFIG.toastDuration })
        this.unsubscribe.push(getMissionSkillListSubscription);
      }

    })
  }

  getMissionThemeList() {
    const getMissionThemeListSubscription = this._service.getMissionThemeList().subscribe({
      next: (data: any) => {

        if (data.result == 1) {
          this.missionThemeList = data.data;
        } else {
          this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration });
        }
      },
      error: (err) => {
        this._toast.error({ detail: "ERROR", summary: err.message, duration: APP_CONFIG.toastDuration })
        this.unsubscribe.push(getMissionThemeListSubscription);
      }
    })
  }

  onSelectedImage(event: any) {
    const files = event.target.files;
    if (this.imageListArray.length > 5) {
      return this._toast.error({ detail: "ERROR", summary: "Maximum 6 images can be added.", duration: APP_CONFIG.toastDuration });
    }
    if (files) {
      this.formData = new FormData();
      for (const file of files) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.imageListArray.push(e.target.result);
        }
        reader.readAsDataURL(file)
      }
      for (let i = 0; i < files.length; i++) {
        this.formData.append('file', files[i]);
        this.formData.append('moduleName', 'Mission');
      }
    }
  }

  async onSubmit() {
    this.formValid = true;
    const value = this.addMissionForm.value;
    let imageUrl: string[] = [];


    // Convert skill IDs to string if it's an array
    if (Array.isArray(value?.missionSkillId)) {
      value.missionSkillId = value.missionSkillId.join(',');
    }

    // Cast fields to their appropriate format.
    value.cityId = Number(value.cityId);
    value.countryId = Number(value.countryId);
    value.missionThemeId = Number(value.missionThemeId);
    value.totalSheets = Number(value.totalSheets);
    value.startDate = new Date(value.startDate).toISOString();
    value.endDate = new Date(value.endDate).toISOString();



    if (this.addMissionForm.valid) {
      // Upload image if any
      if (this.imageListArray.length > 0) {
        try {
          const res: any = await this._commonService.uploadImage(this.formData).toPromise();
          if (res.success) {
            
            imageUrl = Array.isArray(res.data) ? res.data.map((e: any) => String(e)) : [];
            const imgUrlList = imageUrl.map(e => e.trim()).join(",");
            console.log(imgUrlList)
            value.missionImages = imgUrlList;

          }
        } catch (err: any) {
          this._toast.error({ detail: "ERROR", summary: err.message, duration: APP_CONFIG.toastDuration });
        }
      }


      const addMissionSubscription = this._service.addMission(value).subscribe({
        next: (data: any) => {
          if (data.result === 1) {
            this._toast.success({ detail: "SUCCESS", summary: data.data, duration: APP_CONFIG.toastDuration });
            setTimeout(() => this._router.navigate(['admin/mission']), 1000);
          } else {
            this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration });
          }
        },
        error: (err: any) => {
          this._toast.error({ detail: "ERROR", summary: err.message, duration: APP_CONFIG.toastDuration });
        }
      });

      this.formValid = false;
      this.unsubscribe.push(addMissionSubscription);
    }
  }


  onCancel() {
    this._router.navigateByUrl('admin/mission');
  }

  onRemoveImages(item: any) {
    const index: number = this.imageListArray.indexOf(item);
    if (item !== -1) {
      this.imageListArray.splice(index, 1);
    }
  }
  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
