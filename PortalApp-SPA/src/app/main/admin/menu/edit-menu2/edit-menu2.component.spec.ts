import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMenu2Component } from './edit-menu2.component';

describe('EditMenu2Component', () => {
  let component: EditMenu2Component;
  let fixture: ComponentFixture<EditMenu2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditMenu2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditMenu2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
