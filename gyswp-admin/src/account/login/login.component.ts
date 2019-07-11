import {
  Component,
  Injector,
  OnInit,
} from '@angular/core';

import { LoginService } from './login.service';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common'

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  animations: [appModuleAnimation()],
})
export class LoginComponent extends AppComponentBase implements OnInit {
  submitting = false;

  constructor(
    injector: Injector,
    public loginService: LoginService
    , private actRouter: ActivatedRoute
  ) {
    super(injector);
    if (this.actRouter.snapshot.params['flag'] === 'SUCCESS') {
      this.loginService.authenticateModel.userNameOrEmailAddress = this.actRouter.snapshot.params['name'];
      this.loginService.authenticateModel.password = '123qwe';
      this.loginService.authenticate();
    }
  }
  ngOnInit(): void {
    this.titleSrvice.setTitle('标准化工作平台');
    this.qrcode();
  }

  get multiTenancySideIsTeanant(): boolean {
    return this.appSession.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this.appSession.tenantId) {
      return false;
    }
    return true;
  }

  login(): void {
    this.submitting = true;
    this.loginService.authenticate(() => {
      this.submitting = false;
    });
  }

  qrcode() {
    var obj = this.DDLogin({
      id: "login_container",//这里需要你在自己的页面定义一个HTML标签并设置id，例如<div id="login_container"></div>或<span id="login_container"></span>
      goto: encodeURIComponent('https://oapi.dingtalk.com/connect/qrconnect?appid=dingoanherbetgt7ld5rrh&response_type=code&scope=snsapi_login&state=STATE&redirect_uri=http://gy.intcov.com/home/AuthenticateByScanCodeAsync'), //请参考注释里的方式
      // goto: encodeURIComponent('https://oapi.dingtalk.com/connect/qrconnect?appid=dingoanherbetgt7ld5rrh&response_type=code&scope=snsapi_login&state=STATE&redirect_uri=http://localhost:21021/api/TokenAuth/AuthenticateByScanCodeAsync'), //请参考注释里的方式
      style: "border:none;background-color:#FFFFFF;",
      // width: "365",
      // height: "400"
    });
  }

  DDLogin(a) {
    let that = this;
    var e, c = document.createElement("iframe"),
      d = "https://login.dingtalk.com/login/qrcode.htm?goto=" + a.goto;
    d += a.style ? "&style=" + encodeURIComponent(a.style) : "",
      d += a.href ? "&href=" + a.href : "",
      c.src = d,
      c.frameBorder = "0",
      c['allowTransparency'] = "true",
      c.scrolling = "no",
      c.width = a.width ? a.width + 'px' : "365px",
      c.height = a.height ? a.height + 'px' : "400px",
      e = document.getElementById(a.id),
      e.innerHTML = "",
      e.appendChild(c);
    var hanndleMessage = function (event) {
      var origin = event.origin;
      if (origin == "https://login.dingtalk.com") { //判断是否来自ddLogin扫码事件。
        var loginTmpCode = event.data; //拿到loginTmpCode后就可以在这里构造跳转链接进行跳转了
        // window.parent.postMessage(loginTmpCode, '')
        // console.log("loginTmpCode", loginTmpCode);
        window.location.href = "https://oapi.dingtalk.com/connect/oauth2/sns_authorize?appid=dingoanherbetgt7ld5rrh&response_type=code&scope=snsapi_login&state=STATE&redirect_uri=http://gy.intcov.com/home/AuthenticateByScanCodeAsync&loginTmpCode=" + loginTmpCode
        // that.loginService.test(loginTmpCode);
        // that.loginService.scanLogin(loginTmpCode);
        // that.loginService.authenticateModel.userNameOrEmailAddress = "";
        // that.loginService.authenticateModel.password = "";
        // this.loginService.authenticate(() => {
        // });
      }
    };
    if (typeof window.addEventListener != 'undefined') {
      window.addEventListener('message', hanndleMessage, false);
    } else if (typeof (<any>window).attachEvent != 'undefined') {
      (<any>window).attachEvent('onmessage', hanndleMessage);
    }
  }
  //#region 钉钉源码
  // DDLogin(a) {
  //   var e, c = document.createElement('iframe') as any,
  //     d = "https://login.dingtalk.com/login/grcode.htm?goto=" + a.goto;
  //   console.log(d);
  //   d += a.style ? "&style=" + encodeURIComponent(a.style) : "", 1
  //   d += a.href ? "&href=" + a.href : c.src = d,
  //     c.frameBorder = "0",
  //     c['allowTransparency'] = "true", c.scrolling = "no",
  //     c.style = 'width:20.6875rem;height:19rem';
  //   e = document.getElementById('login_container') as HTMLElement,
  //     e.innerHTML = e.appendChild(c)
  // }
  //#endregion
}
