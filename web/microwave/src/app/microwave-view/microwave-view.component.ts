import {Component, OnInit} from '@angular/core';
import {MicrowaveOutput} from "../microwave-template/models/microwave-output";
import {MicrowaveService} from "../microwave-template/microwave.service";

@Component({
  selector: 'app-microwave-view',
  templateUrl: './microwave-view.component.html',
  styleUrls: ['./microwave-view.component.css']
})
export class MicrowaveViewComponent implements OnInit{
  potency: number = 10;
  time: number = 0;
  interval: any;
  progressString: string = '';
  isPaused: boolean = false;
  preConfigurationOptions: MicrowaveOutput[];
  warmingChar: string = '.';
  isTemplate: boolean = false;

  constructor(
    private microwaveService: MicrowaveService) {
  }

  getOptions(){
    this.microwaveService.getAll()
      .subscribe(microwaves => {
        this.preConfigurationOptions = microwaves;
      });
  }

  ngOnInit(): void {
    this.getOptions();
    this.setTemplate();
  }

  startMicrowave(isQuickstart:boolean){
    if(!isQuickstart && !this.isPaused){
      this.progressString = '';
      clearInterval(this.interval);
    }

    this.isPaused = false;
    const progressString = this.buildProgressString(this.potency);

    this.interval = setInterval(() => {
      this.progressString = this.progressString.concat(progressString + ' ');
      this.time--;
      if(this.time == 0)
        this.stopMicrowave();
    },1000)
  }

  buildProgressString(potency: number){
    return Array(potency).fill(this.warmingChar).join('');
  }

  get canStart(){
    return this.potency > 0 && this.potency <= 10 && this.time > 0;
  }

  quickStart() {
    if(this.isPaused){
      this.clearMicrowave();
    }

    if(this.time <= 90){
      this.time = this.time+30;
      clearInterval(this.interval);
    }
    this.startMicrowave(true);
  }

  stopMicrowave() {
    clearInterval(this.interval);

    if(this.time == 0)
      this.progressString = this.progressString.concat('Warming Finished!');
    else if(this.isPaused == true)
      this.clearMicrowave();
    else
      this.isPaused = true;
  }

  public clearMicrowave() {
    this.progressString = '';
    this.warmingChar = '.';
    this.potency = 10;
    this.time = 0;
  }

  setTemplate() {
    const templateControl = document.getElementById('template') as HTMLSelectElement;

    templateControl.addEventListener('change', () => {
      const selectedOption = this.preConfigurationOptions[templateControl.selectedIndex - 1];

      if (selectedOption != null) {
        this.time = selectedOption.time;
        this.potency = selectedOption.potency;
        this.isTemplate = true;
        this.warmingChar = selectedOption.warmingChar;
      } else {
        this.isTemplate = false;
        this.clearMicrowave();
      }
    });
  }
}
