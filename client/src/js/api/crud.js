import axios from "axios";
import api from "./api";
export default {
  async getAll() {
    try {
      return (await axios.get(api.constructUrl("operators"))).data;
    } catch (e) {
      throw e;
    }
  },
  async get(id) {
    try {
      return (await axios.get(api.constructUrl(`operator/${id}`))).data;
    } catch (e) {
      throw e;
    }
  },
  async update(entity) {
    try {
      return (await axios.patch(api.constructUrl(`operator`), entity)).data;
    } catch (e) {
      throw e;
    }
  },
  async create(entity) {
    try {
      return (await axios.post(api.constructUrl(`operator/create`), entity)).data;
    } catch (e) {
      throw e;
    }
  },
  async delete(id) {
    try {
      return (await axios.delete(api.constructUrl(`operator/${id}`))).data;
    } catch (e) {
      throw e;
    }
  }
};
