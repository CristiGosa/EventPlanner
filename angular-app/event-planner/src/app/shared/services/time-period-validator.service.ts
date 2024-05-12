import { Injectable } from '@angular/core';
import {
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

@Injectable()
export class TimePeriodValidatorService {
  constructor() {}

  public isTimePeriodValid: ValidatorFn = (
    group: AbstractControl
  ): ValidationErrors | null => {
    const startDate = group.get('startDate');
    const endDate = group.get('endDate');
    if (startDate?.value > endDate?.value) {
      endDate?.setErrors({ invalidTimePeriod: true });
      return { invalidTimePeriod: true };
    } else {
      endDate?.setErrors(null);
      return null;
    }
  };

  getEndDateErrorMessage(form: AbstractControl): string {
    var errors = form.get('endDate')?.errors;
    if (errors == null) return '';
    if (errors['invalidTimePeriod'] === true) {
      return 'Data de incepere trebuie sa fie inainte datei de incheiere.';
    }
    return '';
  }
}
