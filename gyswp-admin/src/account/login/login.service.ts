import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  ExternalLoginProviderInfoModel,
  ExternalAuthenticateModel,
  ScanUserInfo,
} from '@shared/service-proxies/service-proxies';

import {
  TokenAuthServiceProxy,
  AuthenticateModel,
  AuthenticateResultModel,
} from '@shared/service-proxies/service-proxies';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { AppConsts } from '@shared/AppConsts';

import * as _ from 'lodash';
import { MessageService } from '@abp/message/message.service';
import { LogService } from '@abp/log/log.service';
import { TokenService } from '@abp/auth/token.service';
import { UtilsService } from '@abp/utils/utils.service';
import { finalize } from 'rxjs/operators';

@Injectable()
export class LoginService {
  static readonly twoFactorRememberClientTokenName =
    'TwoFactorRememberClientToken';

  authenticateModel: AuthenticateModel;
  authenticateResult: AuthenticateResultModel;

  rememberMe: boolean;

  constructor(
    private _tokenAuthService: TokenAuthServiceProxy,
    private _router: Router,
    private _utilsService: UtilsService,
    private _messageService: MessageService,
    private _tokenService: TokenService,
    private _logService: LogService,
  ) {
    this.clear();
  }

  // scanLogin(loginTmpCode: string): void {
  //   this._tokenAuthService.scanLogin(loginTmpCode).subscribe((result: ScanUserInfo) => {
  //     console.log(result);
  //   });
  // }
  // test(loginTmpCode: string): void {
  //   this._tokenAuthService.test(loginTmpCode).subscribe((result: ScanUserInfo) => {
  //     console.log(result);
  //   });
  // }
  authenticate(finallyCallback?: () => void): void {
    finallyCallback = finallyCallback || (() => { });

    this._tokenAuthService
      .authenticate(this.authenticateModel)
      .pipe(finalize(finallyCallback))
      .subscribe((result: AuthenticateResultModel) => {
        this.processAuthenticateResult(result);
      });
  }

  private processAuthenticateResult(
    authenticateResult: AuthenticateResultModel,
  ) {
    this.authenticateResult = authenticateResult;

    if (authenticateResult.accessToken) {
      // Successfully logged in
      // tslint:disable-next-line:max-line-length
      this.login(
        authenticateResult.accessToken,
        authenticateResult.encryptedAccessToken,
        authenticateResult.expireInSeconds,
        this.rememberMe,
      );
    } else {
      // Unexpected result!

      this._logService.warn('授权异常!');
      this._router.navigate(['account/login']);
    }
  }

  private login(
    accessToken: string,
    encryptedAccessToken: string,
    expireInSeconds: number,
    rememberMe?: boolean,
  ): void {
    const tokenExpireDate = rememberMe
      ? new Date(new Date().getTime() + 1000 * expireInSeconds)
      : undefined;

    this._tokenService.setToken(accessToken, tokenExpireDate);

    this._utilsService.setCookieValue(
      AppConsts.authorization.encrptedAuthTokenName,
      encryptedAccessToken,
      tokenExpireDate,
      abp.appPath,
    );

    let initialUrl = UrlHelper.initialUrl;
    if (initialUrl.indexOf('/login') > 0) {
      initialUrl = AppConsts.appBaseUrl;
    }

    location.href = initialUrl;
  }

  private clear(): void {
    this.authenticateModel = new AuthenticateModel();
    this.authenticateModel.rememberClient = false;
    this.authenticateResult = null;
    this.rememberMe = false;
  }
}
