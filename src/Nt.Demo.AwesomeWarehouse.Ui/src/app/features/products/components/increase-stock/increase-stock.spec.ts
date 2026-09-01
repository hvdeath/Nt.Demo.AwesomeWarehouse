import { ComponentFixture, TestBed } from '@angular/core/testing';
import { IncreaseStock } from './increase-stock';

describe('IncreaseStock', () => {
  let component: IncreaseStock;
  let fixture: ComponentFixture<IncreaseStock>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncreaseStock],
    }).compileComponents();

    fixture = TestBed.createComponent(IncreaseStock);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
