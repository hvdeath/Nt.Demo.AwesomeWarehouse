import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DecreaseStock } from './decrease-stock';

describe('DecreaseStock', () => {
  let component: DecreaseStock;
  let fixture: ComponentFixture<DecreaseStock>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DecreaseStock],
    }).compileComponents();

    fixture = TestBed.createComponent(DecreaseStock);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
