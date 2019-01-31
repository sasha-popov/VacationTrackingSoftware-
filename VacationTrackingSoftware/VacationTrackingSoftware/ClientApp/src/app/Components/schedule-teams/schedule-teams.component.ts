import { Component, OnInit } from '@angular/core';
import { ScheduleTeamsService } from '../../Services/ScheduleTeams/schedule-teams.service'

@Component({
  selector: 'app-schedule-teams',
  templateUrl: './schedule-teams.component.html',
  styleUrls: ['./schedule-teams.component.css']
})
export class ScheduleTeamsComponent implements OnInit {
  teams: any[];
  countTeams: number;
  constructor(private scheduleTeamsService: ScheduleTeamsService) { }

  ngOnInit() {
    this.showAll();
  }

  showAll() {
    this.scheduleTeamsService.showAll().subscribe(x => {
      this.teams = x;
      this.countTeams = this.teams.length; 
    })
  }

}
