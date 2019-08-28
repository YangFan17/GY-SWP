import { Pipe, PipeTransform } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';

@Pipe({ name: 'hostUrl' })
export class HostUrlPipe implements PipeTransform {
  transform(value: string): string {
    if (value) {
      return AppConsts.remoteServiceBaseUrl + value;
    }
    return value;
  }
}

@Pipe({ name: 'fileUrl' })
export class DocUrlPipe implements PipeTransform {
  transform(value: string): string {
    if (value) {
      return AppConsts.remoteServiceBaseUrl + '/docFile' + value;
    }
    return value;
  }
}