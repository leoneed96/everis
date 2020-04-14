<template>
  <v-container ref="container" fluid grid-list-md>
    <v-layout row>
      <v-flex xs12 ref="statePanel">
        <v-btn color="green darken-1" text @click="onAdd">
          Add operator
        </v-btn>
        <v-card v-for="(item, idx) in operators" :key="idx">
          <v-card-title>
            {{ item.title }}
          </v-card-title>
          <v-card-text> Code : {{ item.code }} </v-card-text>
          <v-card-actions>
            <v-btn color="yellow darken-2" @click="onUpdate(item)" text
              >Update</v-btn
            >
            <v-btn color="red" @click="onDelete(item.id)" text>Delete</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
    <v-snackbar
      v-model="snackbar.show"
    >
      {{ snackbar.text }}
      <v-btn
        color="red"
        text
        @click="snackbar.show = false"
      >
        Close
      </v-btn>
    </v-snackbar>
    <v-dialog v-model="dialog.opened" max-width="450">
      <v-card>
        <v-card-title class="headline">{{ dialog.title }}</v-card-title>
        <v-card-text>
          <v-text-field
            required
            label="Title"
            v-model="dialog.entity.title"
          ></v-text-field>
          <v-text-field
            type="number"
            label="Code"
            v-model="dialog.entity.code"
          ></v-text-field>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="red" text @click="hideDialog">
            Cancel
          </v-btn>

          <v-btn color="green" text @click="onSave">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import crudApi from "../js/api/crud";
export default {
  name: "Main",
  data: () => ({
    operators: [],
    dialog: {
      opened: false,
      title: "",
      entity: {}
    },
    snackbar:{
      show: false,
      text: ""
    }
  }),
  computed: {},
  methods: {
    async onDelete(id) {
      await crudApi.delete(id);
      this.operators = this.operators.filter(a => a.id != id);
    },
    onAdd() {
      this.dialog.title = "Create new operator";
      this.dialog.entity = { title: "", code: "" };
      this.dialog.opened = true;
    },
    onUpdate(item){
      this.dialog.title = `Update "${item.title}"`;
      let editModel = {};
      Object.assign(editModel, item);
      this.dialog.entity = editModel;
      this.dialog.opened = true;
    },
    async onSave() {
      if(!this.dialog.entity.title){
        this.snackbar.text = 'You must specify title';
        this.snackbar.show = true;
        return;
      }
      if (!this.dialog.entity.id) {
        await crudApi.create(this.dialog.entity);
      } else {
        await crudApi.update(this.dialog.entity);
      }
      this.operators = await crudApi.getAll();
      this.dialog.opened = false;
    },
    hideDialog(){
      this.dialog.opened = false;
    }
  },
  watch: {
    "dialog.opened": function(val) {
      if (!val) this.dialog.entity = {};
    }
  },
  async mounted() {
    this.operators = await crudApi.getAll();
  }
};
</script>
